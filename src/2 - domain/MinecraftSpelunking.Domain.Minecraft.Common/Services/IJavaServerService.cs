using MinecraftSpelunking.Common.Services;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Services
{
    public interface IJavaServerService : IBaseEntityService<JavaServer>
    {
        Task<JavaServer> AddAsync(Server info, AddressBlock block);
        Task<JavaServer?> RefreshAsync(Server info);
    }
}
