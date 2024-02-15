using MinecraftSpelunking.Presentation.Common.Models;

namespace MinecraftSpelunking.Presentation.Scanner
{
    public interface IMinecraftSpelunkingApiClient
    {
        Task<AddressBlockAssignmentsModel?> TryGetAsync(int count);
        Task<AddressBlockAssignmentModel?> TryCompleteAsync(AddressBlockAssignmentResultsModel previous);
    }
}
