using MinecraftSpelunking.Presentation.Client.Models;

namespace MinecraftSpelunking.Presentation.Client.Services
{
    public interface IAddressBlockClientService
    {
        Task<AddressBlock?> GetAssignment(AddressBlockResults? result = null);
    }
}
