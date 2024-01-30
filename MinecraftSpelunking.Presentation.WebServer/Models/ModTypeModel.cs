using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Presentation.WebServer.Models
{
    public class ModTypeModel
    {
        public static ModType Empty = new ModType();

        public string Name { get; set; } = string.Empty;
    }
}
