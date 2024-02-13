using MinecraftSpelunking.Common.Database;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Entities
{
    public sealed record ModVersion : BaseEntity
    {
        public static readonly ModVersion Empty = new ModVersion();

        public int ModId { get; set; }
        public Mod Mod { get; set; } = Mod.Empty;
        public string Version { get; set; } = string.Empty;

        public List<JavaServer> JavaServers { get; set; } = [];
    }
}
