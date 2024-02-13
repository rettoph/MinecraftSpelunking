using MinecraftSpelunking.Domain.Identity.Common.Entities;

namespace MinecraftSpelunking.Application.Minecraft.Common.Dtos
{
    public sealed record AddressBlockAssignmentDto
    {
        public int Id { get; set; }
        public AddressBlockDto Block { get; set; } = default!;
        public User User { get; set; } = default!;
        public DateTime AssignedAt { get; set; }
    }
}
