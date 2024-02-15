using MinecraftSpelunking.Application.Identity.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Common.Dtos;

namespace MinecraftSpelunking.Application.Minecraft.Common.Services
{
    public interface IAddressBlockAssignmentApplicationService
    {
        Task<AddressBlockAssignmentDto[]> GetAssignmentsAsync(UserDto userDto, int count);
        Task<bool> TryCompleteAssignmentsAsync(int id, UserDto user, ServerDto[] javaServers);
    }
}
