using Microsoft.Extensions.Logging;
using MinecraftPinger.Library;
using MinecraftPinger.Library.Enums;
using MinecraftPinger.Library.Models;
using MinecraftSpelunking.Presentation.Common.Models;
using System.Net;

namespace MinecraftSpelunking.Presentation.ClientApp.Jobs
{
    internal class ScanJob
    {
        private readonly JavaPinger _java;
        private readonly ILogger<ScanJob> _logger;

        public ScanJob(ILogger<ScanJob> logger)
        {
            _java = new JavaPinger();
            _logger = logger;
        }

        public async Task<AddressBlockAssignmentResultsModel> ScanAsync(AddressBlockAssignmentModel assignment)
        {
            List<ServerModel> javaDiscoveries = new List<ServerModel>();

            IPAddressCollection addresses = assignment.Block.Network.ListIPAddress();

            _logger.LogInformation("Scanning {Network}; {Count} addresses.", assignment.Block.Network, addresses.Count);


            int count = 0;
            Pong<HandshakeResponse> pong;
            foreach (IPAddress address in addresses)
            {
                if ((pong = await this.PingJava(assignment, address, 25565)).Status != PongStatusEnum.Timeout)
                {
                    javaDiscoveries.Add(new ServerModel()
                    {
                        Host = pong.Endpoint.Address.ToString(),
                        Port = pong.Endpoint.Port
                    });
                }

                if (++count % 256 == 0)
                {
                    _logger.LogDebug("Scanning {Network}; {Count}/{Total}", assignment.Block.Network, count, addresses.Count);
                }
                else
                {
                    _logger.LogTrace("Scanning {Network}; {Count}/{Total}", assignment.Block.Network, count, addresses.Count);
                }
            }

            _logger.LogInformation("Done scanning {Network}; {JavaCount} Java server(s) discovered.", assignment.Block.Network, javaDiscoveries.Count);

            return new AddressBlockAssignmentResultsModel()
            {
                Id = assignment.Id,
                JavaServers = javaDiscoveries.ToArray()
            };
        }

        private async Task<Pong<HandshakeResponse>> PingJava(AddressBlockAssignmentModel assignment, IPAddress address, int port)
        {
            Pong<HandshakeResponse> pong = await _java.PingAsync(new IPEndPoint(address, port), 250);

            if (pong.Status == PongStatusEnum.OK)
            {
                _logger.LogInformation("Found Java Server at {Network}; Address = {Address}, Port = {Port}", assignment.Block.Network, pong.Endpoint.Address.ToString(), pong.Endpoint.Port);
            }
            else if (pong.Status == PongStatusEnum.Exception)
            {
                _logger.LogError(pong.Exception, "Unknown error at {Network}; Address = {Address}, Port = {Port}", assignment.Block.Network, pong.Endpoint.Address.ToString(), pong.Endpoint.Port);
            }

            return pong;
        }
    }
}
