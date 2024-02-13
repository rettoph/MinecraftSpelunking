using MinecraftPinger.Library.Models;
using MinecraftSpelunking.Common.Database;
using MinecraftSpelunking.Domain.Minecraft.Common.Enums;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Entities
{
    public sealed record JavaServer : BaseEntity
    {
        public static readonly JavaServer Empty = new();

        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }

        public string Version { get; set; } = string.Empty;
        public int PlayersOnline { get; set; }
        public int PlayersMax { get; set; }
        public Player[] PlayersSample { get; set; } = Array.Empty<Player>();
        public Chat Description { get; set; } = Chat.Empty;
        public string DescriptionNormalized { get; set; } = string.Empty;
        public ModType? ModType { get; set; }
        public List<ModVersion> ModVersions { get; set; } = [];
        public ModPackData? ModPackData { get; set; }

        public ServerIcon? Icon { get; set; }
        public AddressBlock AddressBlock { get; set; } = default!;

        public ServerStatusEnum Status { get; set; }

        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public DateTime LastOnlineAt { get; set; }
    }
}
