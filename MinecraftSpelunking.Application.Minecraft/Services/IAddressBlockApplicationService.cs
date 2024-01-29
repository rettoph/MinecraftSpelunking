using MinecraftSpelunking.Application.Minecraft.Dtos;
using MinecraftSpelunking.Common.Account.Entities;

namespace MinecraftSpelunking.Application.Minecraft.Services
{
    public interface IAddressBlockApplicationService
    {
        void Complete(int id, ServerDto[] javaDiscoveries);

        AddressBlockDto GetAssignment(User user);
    }
}
