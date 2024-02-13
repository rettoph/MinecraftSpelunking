namespace MinecraftSpelunking.Application.Minecraft.Common.Dtos
{
    public sealed record ServerIconDto
    {
        public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public int Hash { get; set; }
    }
}
