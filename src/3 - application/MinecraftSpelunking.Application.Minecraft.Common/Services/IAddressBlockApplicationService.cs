using MinecraftSpelunking.Application.Identity.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Common.Dtos;

namespace MinecraftSpelunking.Application.Minecraft.Common.Services
{
    public interface IAddressBlockApplicationService
    {
        Task<AddressBlockAssignmentDto?> GetAssignmentAsync(UserDto userDto);
    }
}
