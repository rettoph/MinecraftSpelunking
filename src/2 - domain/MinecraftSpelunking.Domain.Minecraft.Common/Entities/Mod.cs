using MinecraftSpelunking.Common.Database;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Entities
{
    public sealed record Mod : BaseEntity
    {
        public static readonly Mod Empty = new Mod();

        public string Name { get; set; } = string.Empty;
    }
}
