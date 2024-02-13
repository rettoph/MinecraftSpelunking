using MinecraftSpelunking.Common.Database;
using MinecraftSpelunking.Domain.Identity.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Entities
{
    public sealed record AddressBlockAssignment : BaseEntity
    {
        public AddressBlock Block { get; set; } = default!;
        public User User { get; set; } = default!;
        public DateTime AssignedAt { get; set; }
    }
}
