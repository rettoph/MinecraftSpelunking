using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Common.Minecraft.Services
{
    public interface IJavaServerService
    {
        Task<JavaServer> GetByIdAsync(int id);

        Task<JavaServer> RefreshAsync(int id);

        Task<JavaServer> AddAsync(string host, int port, AddressBlock block);

        Task<JavaServer[]> GetAllAsync(int count, int offset);
    }
}
