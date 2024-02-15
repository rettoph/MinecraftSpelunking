namespace MinecraftSpelunking.Presentation.Common.Models
{
    public sealed class AddressBlockAssignmentResultsModel
    {
        public int Id { get; set; }
        public ServerModel[] JavaServers { get; set; } = Array.Empty<ServerModel>();
    }
}
