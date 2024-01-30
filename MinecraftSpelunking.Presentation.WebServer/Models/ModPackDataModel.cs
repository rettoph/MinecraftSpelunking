namespace MinecraftSpelunking.Presentation.WebServer.Models
{
    public class ModPackDataModel
    {
        public int ProjectId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        public int VersionId { get; set; }

        public bool IsMetadata { get; set; }
    }
}
