using MinecraftSpelunking.Application.Identity.Common.Dtos;

namespace MinecraftSpelunking.Application.Minecraft.Common.Dtos
{
    public sealed record AddressBlockAssignmentDto
    {
        public int Id { get; set; }
        public AddressBlockDto Block { get; set; } = default!;
        public UserDto User { get; set; } = default!;
        public DateTime AssignedAt { get; set; }
    }
}
