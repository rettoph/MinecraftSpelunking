namespace MinecraftSpelunking.Common.Minecraft.Entities
{
    public class ModType : BaseEntity
    {
        public static ModType Empty = new ModType();

        public string Name { get; set; } = string.Empty;
    }
}
