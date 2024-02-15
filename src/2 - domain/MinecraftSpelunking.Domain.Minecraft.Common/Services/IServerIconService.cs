using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Services
{
    public interface IServerIconService
    {
        int GetBase64Hash(string base64);

        ServerIcon Add(string base64);
    }
}
