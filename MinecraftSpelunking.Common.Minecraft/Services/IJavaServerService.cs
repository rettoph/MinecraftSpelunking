using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Common.Minecraft.Services
{
    public interface IJavaServerService
    {
        JavaServer Add(string host, int port, AddressBlock block);
    }
}
