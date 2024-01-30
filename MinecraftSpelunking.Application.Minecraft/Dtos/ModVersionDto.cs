using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Application.Minecraft.Dtos
{
    public class ModVersionDto
    {
        public static readonly ModVersion Empty = new ModVersion();

        public int ModId { get; set; }
        public ModDto Mod { get; set; } = ModDto.Empty;
        public string Version { get; set; } = string.Empty;
    }
}
