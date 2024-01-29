using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Common.Minecraft.Services
{
    public interface IServerIconService
    {
        ServerIcon Add(string base64);

        int GetBase64Hash(string base64);
    }
}
