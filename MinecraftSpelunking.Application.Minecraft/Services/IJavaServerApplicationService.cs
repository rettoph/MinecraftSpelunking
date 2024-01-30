using MinecraftSpelunking.Application.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Dtos;

namespace MinecraftSpelunking.Application.Minecraft.Services
{
    public interface IJavaServerApplicationService
    {
        Task<JavaServerDto> GetByIdAsync(int id);

        Task<JavaServerDto> RefreshAsync(int id);

        Task<PageDto<JavaServerDto>> GetAllAsync(int page, int size = 100);

        Task<JavaServerDto> AddAsync(string host, int port, AddressBlockDto block);
    }
}
