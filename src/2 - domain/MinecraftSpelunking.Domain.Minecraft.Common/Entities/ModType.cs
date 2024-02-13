using MinecraftSpelunking.Common.Database;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Entities
{
    public sealed record ModType : BaseEntity
    {
        public static ModType Empty = new ModType();

        public string Name { get; set; } = string.Empty;
    }
}
