namespace MinecraftSpelunking.Presentation.Common.Models
{
    public class AddressBlockAssignmentsModel
    {
        public required AddressBlockAssignmentModel[] Assignments { get; set; } = Array.Empty<AddressBlockAssignmentModel>();
    }
}
