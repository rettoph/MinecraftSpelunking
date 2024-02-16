namespace MinecraftSpelunking.Presentation.ClientApp
{
    public sealed class MinecraftSpelunkingClientConfiguration
    {
        public string BaseAddress { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public int ConcurrentTasks { get; set; } = 128;
        public int RunTimeInHours { get; set; } = 5;
    }
}
