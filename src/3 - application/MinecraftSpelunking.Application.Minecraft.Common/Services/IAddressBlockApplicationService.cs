using MinecraftSpelunking.Application.Minecraft.Common.Dtos;
using MinecraftSpelunking.Common;
using MinecraftSpelunking.Domain.Minecraft.Common.Enums;
using System.Net;

namespace MinecraftSpelunking.Application.Minecraft.Common.Services
{
    public interface IAddressBlockApplicationService
    {
        Task<Page<AddressBlockDto>> GetAddressBlocksAsync(int page, int size, string query);

        Task SetStatusAsync(int id, AddressBlockStatusEnum status);

        Task<int> BulkSetStatusAsync(IEnumerable<IPNetwork2> networks, AddressBlockStatusEnum status);
    }
}
