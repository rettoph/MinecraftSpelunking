namespace MinecraftSpelunking.Application.Minecraft.Common.Dtos
{
    public sealed record ServerDto
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
