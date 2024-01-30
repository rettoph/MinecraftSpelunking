namespace MinecraftSpelunking.Presentation.WebServer.Models
{
    public class ModVersionModel
    {
        public static readonly ModVersionModel Empty = new ModVersionModel();

        public int ModId { get; set; }
        public ModModel Mod { get; set; } = ModModel.Empty;
        public string Version { get; set; } = string.Empty;
    }
}
