using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Application.Minecraft.Dtos
{
    public class ModTypeDto
    {
        public static ModType Empty = new ModType();

        public string Name { get; set; } = string.Empty;
    }
}
