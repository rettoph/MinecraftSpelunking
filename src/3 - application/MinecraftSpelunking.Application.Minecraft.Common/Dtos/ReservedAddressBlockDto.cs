using System.Net;

namespace MinecraftSpelunking.Application.Minecraft.Common.Dtos
{
    public sealed record ReservedAddressBlockDto
    {
        public int Id { get; set; }
        public IPNetwork2 Network { get; set; } = default!;
    }
}
