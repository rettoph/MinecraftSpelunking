namespace MinecraftSpelunking.Application.Minecraft.Common.Dtos
{
    public sealed record ModTypeDto
    {
        public static ModTypeDto Empty = new ModTypeDto();

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
