using MinecraftSpelunking.Common.Services;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Services
{
    public interface IAddressBlockService : IBaseEntityService<AddressBlock>
    {
        Task<AddressBlock?> GetAssignableAddressBlockAsync();

        Task<AddressBlockAssignment?> TryAssignAddressBlockAsync(AddressBlock block, User user);
    }
}
