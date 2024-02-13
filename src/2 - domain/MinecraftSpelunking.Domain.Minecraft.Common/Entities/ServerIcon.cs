using MinecraftSpelunking.Common.Database;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Entities
{
    public sealed record ServerIcon : BaseEntity
    {
        public string Path { get; set; } = string.Empty;
        public int Hash { get; set; }
    }
}
