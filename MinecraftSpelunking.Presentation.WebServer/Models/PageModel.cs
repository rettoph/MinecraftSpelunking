namespace MinecraftSpelunking.Presentation.WebServer.Models
{
    public class PageModel<T>
    {
        public required int Number { get; init; }
        public required int Size { get; init; }
        public required T[] Items { get; init; }
    }
}
