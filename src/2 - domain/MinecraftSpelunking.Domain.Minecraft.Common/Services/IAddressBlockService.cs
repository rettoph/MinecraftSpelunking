using MinecraftSpelunking.Common.Services;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Enums;
using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Services
{
    public interface IAddressBlockService : IBaseEntityService<AddressBlock>
    {
        Task<AddressBlock?> GetAssignableAddressBlockAsync();

        Task EnsureAllBlocksExistAsync();

        Task SetStatusAsync(int id, AddressBlockStatusEnum status);
        Task<int> BulkSetStatusAsync(IEnumerable<IPNetwork2> networks, AddressBlockStatusEnum status);
    }
}
