using MinecraftSpelunking.Common.Database;
using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Entities
{
    public sealed record ReservedAddressBlock : BaseEntity
    {
        public IPNetwork2 Network { get; set; } = default!;
    }
}
