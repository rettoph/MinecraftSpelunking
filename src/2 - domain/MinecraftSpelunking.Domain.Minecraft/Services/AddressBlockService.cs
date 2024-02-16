using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinecraftSpelunking.Domain.Common.Services;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Enums;
using MinecraftSpelunking.Domain.Minecraft.Common.Services;
using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Services
{
    internal sealed class AddressBlockService : BaseEntityService<AddressBlock>, IAddressBlockService
    {
        private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1);

        private readonly static TimeSpan StaleInterval = TimeSpan.FromMinutes(60);

        private readonly IServiceProvider _services;
        private readonly IReservedAddressBlockService _reservedAddressBlocks;

        public AddressBlockService(IServiceProvider services, IReservedAddressBlockService reservedAddressBlocks, DataContext context, IMapper mapper) : base(x => x.AddressBlocks, context, mapper)
        {
            _services = services;
            _reservedAddressBlocks = reservedAddressBlocks;
        }

        public async Task<AddressBlock?> GetAssignableAddressBlockAsync()
        {
            await _lock.WaitAsync();

            try
            {
                int passes = 0;
                while (passes <= 1)
                {
                    // Check for any assigned blocks that have gone stale
                    DateTime staleTime = DateTime.Now.Subtract(StaleInterval);
                    AddressBlock? stale = await this.entities
                        .Where(x => x.Status == AddressBlockStatusEnum.Assigned)
                        .Where(x => staleTime >= x.ModifiedAt)
                        .FirstOrDefaultAsync();

                    if (stale is not null)
                    {
                        return stale;
                    }

                    // Check for any requested blocks
                    AddressBlock? requested = await this.entities
                        .Where(x => x.Status == AddressBlockStatusEnum.Requested)
                        .FirstOrDefaultAsync();

                    if (requested is not null)
                    {
                        return requested;
                    }

                    // Check for any available blocks
                    AddressBlock? available = await this.entities
                        .Where(x => x.Status == AddressBlockStatusEnum.Available)
                        .FirstOrDefaultAsync();

                    if (available is not null)
                    {
                        return available;
                    }

                    if (passes > 0)
                    {
                        return null;
                    }

                    await this.context.Database.ExecuteSqlRawAsync($"update dbo.AddressBlocks set Status = {(int)AddressBlockStatusEnum.Available};");
                    passes++;
                }

                return null;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task EnsureAllBlocksExistAsync()
        {
            await _lock.WaitAsync();

            try
            {
                if (this.entities.Any())
                {
                    return;
                }

                int index = 0;
                List<AddressBlock> batch = new List<AddressBlock>();
                for (int i = 0; i < 5000; i++)
                {
                    batch.Add(new AddressBlock());
                }

                IPNetwork2? network = IPNetwork2.Parse($"0.0.0.0/{AddressBlock.CIDR}");
                while (network is not null)
                {
                    if (_reservedAddressBlocks.IsReserved(network) == false)
                    {
                        AddressBlock block = batch[index++];

                        block.Id = default;
                        block.Status = AddressBlockStatusEnum.Available;
                        block.Network = network;
                        block.CreatedAt = DateTime.Now;
                        block.ModifiedAt = DateTime.Now;

                        if (index >= batch.Count)
                        {
                            await this.InsertBatchAsync(batch);
                            index = 0;
                        }
                    }

                    network = network.CalculateNextNetwork();
                }

                await this.InsertBatchAsync(batch.Take(index));
            }
            finally
            {
                _lock.Release();
            }
        }

        private async Task InsertBatchAsync(IEnumerable<AddressBlock> batch)
        {
            using (var scope = _services.CreateScope())
            {
                using (var batchContext = scope.ServiceProvider.GetRequiredService<DataContext>())
                {
                    await batchContext.BulkInsertOptimizedAsync(batch);
                }
            }
        }

        public async Task SetStatusAsync(int id, AddressBlockStatusEnum status)
        {
            await this.entities.Where(x => x.Id == id).ExecuteUpdateAsync(u => u.SetProperty(x => x.Status, status));
            await this.context.SaveChangesAsync();
        }

        public async Task<int> BulkSetStatusAsync(IEnumerable<IPNetwork2> networks, AddressBlockStatusEnum status)
        {
            AddressBlock first = this.entities.First(x => x.Network == networks.First());
            AddressBlock last = this.entities.First(x => x.Network == networks.Last());

            return await this.context.Database.ExecuteSqlAsync($"update dbo.AddressBlocks set Status = {(int)status} where Id between {first.Id} and {last.Id} and Status = {(int)AddressBlockStatusEnum.Available};");
        }
    }
}
