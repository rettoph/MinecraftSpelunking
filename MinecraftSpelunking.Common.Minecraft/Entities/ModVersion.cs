namespace MinecraftSpelunking.Common.Minecraft.Entities
{
    public class ModVersion : BaseEntity
    {
        public static readonly ModVersion Empty = new ModVersion();

        public int ModId { get; set; }
        public Mod Mod { get; set; } = Mod.Empty;
        public string Version { get; set; } = string.Empty;

        public List<JavaServer> JavaServers { get; set; } = [];
    }
}
