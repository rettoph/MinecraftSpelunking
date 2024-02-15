using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Services
{
    public interface IAddressBlockAssignmentService
    {
        Task<AddressBlockAssignment[]> GetAssignmentsAsync(User user, int count);

        Task<bool> TryCompleteAssignmentsAsync(int id, User user, Server[] javaServers);
    }
}
