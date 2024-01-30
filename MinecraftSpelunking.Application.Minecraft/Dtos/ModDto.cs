namespace MinecraftSpelunking.Application.Minecraft.Dtos
{
    public class ModDto
    {
        public static readonly ModDto Empty = new ModDto();

        public string Name { get; set; } = string.Empty;
    }
}
