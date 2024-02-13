namespace MinecraftSpelunking.Application.Minecraft.Common.Dtos
{
    public sealed record ModDto
    {
        public static readonly ModDto Empty = new ModDto();

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
