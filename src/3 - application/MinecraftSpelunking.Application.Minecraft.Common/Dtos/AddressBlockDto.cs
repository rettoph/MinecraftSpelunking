using MinecraftSpelunking.Domain.Minecraft.Common.Enums;
using System.Net;

namespace MinecraftSpelunking.Application.Minecraft.Common.Dtos
{
    public sealed record AddressBlockDto
    {
        public int Id { get; set; }

        public IPNetwork2 Network { get; set; } = default!;

        public AddressBlockStatusEnum Status { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
