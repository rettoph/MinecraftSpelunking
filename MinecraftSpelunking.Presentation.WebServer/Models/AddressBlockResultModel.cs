namespace MinecraftSpelunking.Presentation.WebServer.Models
{
    public class AddressBlockResultModel
    {
        public int Id { get; set; }
        public ServerModel[] JavaServers { get; set; } = Array.Empty<ServerModel>();
    }
}
