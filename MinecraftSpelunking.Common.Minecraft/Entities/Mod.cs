namespace MinecraftSpelunking.Common.Minecraft.Entities
{
    public class Mod : BaseEntity
    {
        public static readonly Mod Empty = new Mod();

        public string Name { get; set; } = string.Empty;
    }
}
