using MinecraftSpelunking.Common.Minecraft.Enums;

namespace MinecraftSpelunking.Presentation.WebServer.Models
{
    public sealed class JavaServerModel
    {
        public int Id { get; set; }

        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }

        public string Version { get; set; } = string.Empty;
        public int PlayersOnline { get; set; }
        public int PlayersMax { get; set; }
        public PlayerModel[] PlayersSample { get; set; } = Array.Empty<PlayerModel>();
        public ChatModel Description { get; set; } = ChatModel.Empty;
        public string DescriptionNormalized { get; set; } = string.Empty;
        public ModTypeModel? ModType { get; set; }
        public ModVersionModel[] ModVersions { get; set; } = [];
        public ModPackDataModel? ModPackData { get; set; }
        public ServerIconModel? Icon { get; set; }

        public ServerStatusEnum Status { get; set; }

        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public DateTime LastOnlineAt { get; set; }
    }
}
