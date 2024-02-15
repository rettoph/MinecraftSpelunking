using MinecraftSpelunking.Common.Database;
using MinecraftSpelunking.Domain.Minecraft.Common.Enums;
using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Entities
{
    public sealed record AddressBlock : BaseEntity
    {
        public const byte CIDR = 21;

        public IPNetwork2 Network { get; set; } = default!;

        public AddressBlockStatusEnum Status { get; set; }
    }
}
