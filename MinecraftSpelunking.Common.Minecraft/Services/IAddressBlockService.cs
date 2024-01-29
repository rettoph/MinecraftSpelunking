using MinecraftSpelunking.Common.Account.Entities;
using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Common.Minecraft.Services
{
    public interface IAddressBlockService
    {
        AddressBlock GetById(int id);

        void Complete(int id, Server[] javaDiscoveries);

        AddressBlock GetAssignment(User user);
    }
}
