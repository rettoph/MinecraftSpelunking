namespace MinecraftSpelunking.Presentation.WebServer.Models
{
    public class ModModel
    {
        public static readonly ModModel Empty = new ModModel();

        public string Name { get; set; } = string.Empty;
    }
}
