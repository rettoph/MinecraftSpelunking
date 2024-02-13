using MinecraftPinger.Library.Models;
using MinecraftSpelunking.Domain.Minecraft.Common.Enums;

namespace MinecraftSpelunking.Application.Minecraft.Common.Dtos
{
    public sealed record JavaServerDto
    {
        public static readonly JavaServerDto Empty = new();

        public int Id { get; set; }
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }

        public string Version { get; set; } = string.Empty;
        public int PlayersOnline { get; set; }
        public int PlayersMax { get; set; }
        public Player[] PlayersSample { get; set; } = Array.Empty<Player>();
        public Chat Description { get; set; } = Chat.Empty;
        public string DescriptionNormalized { get; set; } = string.Empty;
        public ModTypeDto? ModType { get; set; }
        public List<ModVersionDto> ModVersions { get; set; } = [];
        public ModPackDataDto? ModPackData { get; set; }

        public ServerIconDto? Icon { get; set; }
        public AddressBlockDto AddressBlock { get; set; } = default!;

        public ServerStatusEnum Status { get; set; }

        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public DateTime LastOnlineAt { get; set; }
    }
}
