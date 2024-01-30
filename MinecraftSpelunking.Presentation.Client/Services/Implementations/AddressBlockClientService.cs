using MinecraftSpelunking.Presentation.Client.Models;

namespace MinecraftSpelunking.Presentation.Client.Services.Implementations
{
    internal sealed class AddressBlockClientService : IAddressBlockClientService
    {
        private const string GetAssignmentPath = "/api/v1/address-block/get-assignment";

        private readonly IMinecraftSpelunkingClientService _client;

        public AddressBlockClientService(IMinecraftSpelunkingClientService client)
        {
            _client = client;
        }

        public async Task<AddressBlock?> GetAssignment(AddressBlockResults? result = null)
        {
            return (await _client.PostAsync<AddressBlockResults?, AddressBlock>(GetAssignmentPath, result)).Content;
        }
    }
}
