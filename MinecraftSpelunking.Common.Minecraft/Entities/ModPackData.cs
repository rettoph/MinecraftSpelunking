namespace MinecraftSpelunking.Common.Minecraft.Entities
{
    public class ModPackData : BaseEntity
    {
        public int ProjectId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        public int VersionId { get; set; }

        public bool IsMetadata { get; set; }
    }
}
