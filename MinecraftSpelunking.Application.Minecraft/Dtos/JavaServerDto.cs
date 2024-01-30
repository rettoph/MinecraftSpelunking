using MinecraftSpelunking.Common.Minecraft.Enums;

namespace MinecraftSpelunking.Application.Minecraft.Dtos
{
    public class JavaServerDto
    {
        public int Id { get; set; }

        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }

        public string Version { get; set; } = string.Empty;
        public int PlayersOnline { get; set; }
        public int PlayersMax { get; set; }
        public PlayerDto[] PlayersSample { get; set; } = Array.Empty<PlayerDto>();
        public ChatDto Description { get; set; } = ChatDto.Empty;
        public string DescriptionNormalized { get; set; } = string.Empty;
        public ModTypeDto? ModType { get; set; }
        public ModVersionDto[] ModVersions { get; set; } = [];
        public ModPackDataDto? ModPackData { get; set; }
        public ServerIconDto? Icon { get; set; }

        public ServerStatusEnum Status { get; set; }

        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public DateTime LastOnlineAt { get; set; }
    }
}
