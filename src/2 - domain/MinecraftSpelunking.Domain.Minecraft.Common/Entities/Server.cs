namespace MinecraftSpelunking.Domain.Minecraft.Common.Entities
{
    public sealed record Server
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
