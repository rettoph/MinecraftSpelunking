namespace MinecraftSpelunking.Presentation.WebServer.Models
{
    public class AddressBlockAssignmentModel
    {
        public int Id { get; set; }
        public AddressBlockModel Block { get; set; } = default!;
        public UserModel User { get; set; } = default!;
        public DateTime AssignedAt { get; set; }
    }
}
