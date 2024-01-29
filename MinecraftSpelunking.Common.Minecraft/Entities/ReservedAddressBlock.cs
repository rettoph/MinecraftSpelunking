using System.Net;

namespace MinecraftSpelunking.Common.Minecraft.Entities
{
    public class ReservedAddressBlock : BaseEntity
    {
        public IPNetwork2 Network { get; set; } = default!;
    }
}
