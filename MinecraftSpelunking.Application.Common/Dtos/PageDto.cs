namespace MinecraftSpelunking.Application.Common.Dtos
{
    public class PageDto<T>
    {
        public int Number { get; set; }
        public int Size { get; set; }
        public T[] Items { get; set; } = Array.Empty<T>();
    }
}
