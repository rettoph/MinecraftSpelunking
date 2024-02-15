using MinecraftSpelunking.Common.Services;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Services
{
    public interface IAddressBlockService : IBaseEntityService<AddressBlock>
    {
        Task<AddressBlock?> GetAssignableAddressBlockAsync();
    }
}
