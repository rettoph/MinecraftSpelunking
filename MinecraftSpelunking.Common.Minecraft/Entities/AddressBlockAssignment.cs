using MinecraftSpelunking.Common.Account.Entities;

namespace MinecraftSpelunking.Common.Minecraft.Entities
{
    public class AddressBlockAssignment : BaseEntity
    {
        public AddressBlock Block { get; set; } = default!;
        public User User { get; set; } = default!;
        public DateTime AssignedAt { get; set; }
    }
}
