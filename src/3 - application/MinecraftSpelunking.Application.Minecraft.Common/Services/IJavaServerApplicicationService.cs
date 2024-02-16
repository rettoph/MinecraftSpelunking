using MinecraftSpelunking.Application.Minecraft.Common.Dtos;
using MinecraftSpelunking.Common;

namespace MinecraftSpelunking.Application.Minecraft.Common.Services
{
    public interface IJavaServerApplicicationService
    {
        Task<JavaServerDto?> GetJavaServerAsync(int id);
        Task<Page<JavaServerDto>> GetJavaServersAsync(int page, int size, string query);
    }
}
