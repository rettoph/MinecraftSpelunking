using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Domain.Common.Services;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
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

        private readonly IReservedAddressBlockService _reservedAddressBlocks;

        public AddressBlockService(IReservedAddressBlockService reservedAddressBlocks, DataContext context, IMapper mapper) : base(x => x.AddressBlocks, context, mapper)
        {
            _reservedAddressBlocks = reservedAddressBlocks;
        }

        public async Task<AddressBlock?> GetAssignableAddressBlockAsync()
        {
            try
            {
                await _lock.WaitAsync();

                // Check for any address blocks that have gone stale
                DateTime staleTime = DateTime.Now.Subtract(StaleInterval);
                AddressBlock? stale = await this.entities
                    .Where(x => x.Status != AddressBlockStatusEnum.Scanned)
                    .Where(x => staleTime >= x.ModifiedAt)
                    .FirstOrDefaultAsync();

                if (stale is not null)
                {
                    return stale;
                }

                AddressBlock? last = await this.entities.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                IPNetwork2? network = default!;
                if (last is null)
                {
                    network = IPNetwork2.Parse($"0.0.0.0/{AddressBlock.CIDR}");
                }
                else
                {
                    network = last.Network.CalculateNextNetwork();
                }

                AddressBlock? next = null;
                do
                {
                    if (network is null)
                    {
                        next = null;
                        break;
                    }

                    next = new AddressBlock()
                    {
                        Network = network,
                        Status = _reservedAddressBlocks.IsReserved(network) ? AddressBlockStatusEnum.Reserved : AddressBlockStatusEnum.Available,
                        ModifiedAt = DateTime.Now
                    };

                    this.entities.Add(next);

                    if (next.Status == AddressBlockStatusEnum.Reserved)
                    {
                        network = network.CalculateNextNetwork();
                    }
                    else if (next.Status == AddressBlockStatusEnum.Available)
                    {
                        next.Status = AddressBlockStatusEnum.Available;
                    }
                } while (next.Status != AddressBlockStatusEnum.Available);

                if (next is not null)
                {
                    await this.context.SaveChangesAsync();
                }

                return next;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<AddressBlockAssignment?> TryAssignAddressBlockAsync(AddressBlock block, User user)
        {
            block.Status = AddressBlockStatusEnum.Assigned;
            AddressBlockAssignment assignment = new AddressBlockAssignment()
            {
                User = user,
                Block = block,
                AssignedAt = DateTime.Now
            };

            await this.context.SaveChangesAsync();

            return assignment;
        }
    }
}
