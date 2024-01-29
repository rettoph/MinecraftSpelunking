using MinecraftPinger.Library.Models;
using MinecraftSpelunking.Common.Minecraft.Enums;

namespace MinecraftSpelunking.Common.Minecraft.Entities
{
    public class JavaServer : BaseEntity
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }

        public string Version { get; set; } = string.Empty;
        public int PlayersOnline { get; set; }
        public int PlayersMax { get; set; }
        public Player[] PlayersSample { get; set; } = Array.Empty<Player>();
        public Chat Description { get; set; } = Chat.Empty;
        public ServerIcon? Icon { get; set; }
        public AddressBlock AddressBlock { get; set; } = default!;

        public ServerStatusEnum Status { get; set; }

        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public DateTime LastOnlineAt { get; set; }
    }
}
