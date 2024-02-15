using Microsoft.IdentityModel.Tokens;
using MinecraftSpelunking.Application.Minecraft.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Common.Services;
using MinecraftSpelunking.Common;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Enums;
using MinecraftSpelunking.Domain.Minecraft.Common.Services;
using System.Net;

namespace MinecraftSpelunking.Application.Minecraft.Services
{
    internal sealed class AddressBlockApplicationService : IAddressBlockApplicationService
    {
        private readonly IAddressBlockService _blocks;
        private readonly DataContext _context;

        public AddressBlockApplicationService(IAddressBlockService blocks, DataContext context)
        {
            _blocks = blocks;
            _context = context;
        }

        public Task<Page<AddressBlockDto>> GetAddressBlocksAsync(int number, int size, string query)
        {
            IQueryable<AddressBlock> queryable = _blocks.AsQueryable();
            if (query.IsNullOrEmpty() == false)
            {
                queryable = queryable.Where(x => ((string)(object)x.Network).Contains(query));
            }

            Page<AddressBlock> page = queryable.Page(number, size);
            Page<AddressBlockDto> pageDto = _blocks.Map<AddressBlockDto>(page);

            return Task.FromResult(pageDto);
        }

        public async Task SetStatusAsync(int id, AddressBlockStatusEnum status)
        {
            await _blocks.SetStatusAsync(id, status);
        }

        public async Task<int> BulkSetStatusAsync(IEnumerable<IPNetwork2> networks, AddressBlockStatusEnum status)
        {
            return await _blocks.BulkSetStatusAsync(networks, status);
        }
    }
}
