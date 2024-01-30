using AutoMapper;
using Microsoft.Extensions.Logging;
using MinecraftPinger.Library;
using MinecraftPinger.Library.Enums;
using MinecraftPinger.Library.Models;
using MinecraftSpelunking.Presentation.Client.Models;
using MinecraftSpelunking.Presentation.Client.Services;
using System.Net;

namespace MinecraftSpelunking.Presentation.Client.Jobs
{
    public class PingJob
    {
        private readonly JavaPinger _java;
        private readonly IAddressBlockClientService _addressBlocks;
        private readonly ILogger<PingJob> _logger;
        private readonly IMapper _mapper;

        public PingJob(IAddressBlockClientService addressBlocks, IMapper mapper, ILogger<PingJob> logger)
        {
            _java = new JavaPinger();
            _addressBlocks = addressBlocks;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task StartAsync()
        {
            AddressBlock? assignment = await _addressBlocks.GetAssignment();

            if (assignment is null)
            {
                _logger.LogWarning($"No initial assignment recieved. Killing job.");
                return;
            }

            List<Server> javaDiscoveries = new List<Server>();

            while (assignment is not null)
            {
                IPAddressCollection addresses = IPNetwork2.Parse(assignment.Network).ListIPAddress();

                _logger.LogInformation("Scanning {Network}; {Count} addresses.", assignment.Network, addresses.Count);


                int count = 0;
                Pong<HandshakeResponse> pong = default!;
                foreach (IPAddress address in addresses)
                {
                    if ((pong = await this.PingJava(assignment, address, 25565)).Status != PongStatusEnum.Timeout)
                    {
                        Server server = _mapper.Map<Server>(pong);
                        javaDiscoveries.Add(server);
                    }

                    if (++count % 256 == 0)
                    {
                        _logger.LogInformation("Scanning {Network}; {Count}/{Total}", assignment.Network, count, addresses.Count);
                    }
                }

                _logger.LogInformation("Done scanning {Network}; {JavaCount} Java server(s) discovered.", assignment.Network, javaDiscoveries.Count);

                AddressBlockResults results = new AddressBlockResults()
                {
                    Id = assignment.Id,
                    JavaServers = javaDiscoveries.ToArray()
                };

                assignment = await _addressBlocks.GetAssignment(results);
            }
        }

        private async Task<Pong<HandshakeResponse>> PingJava(AddressBlock assignment, IPAddress address, int port)
        {
            Pong<HandshakeResponse> pong = await _java.PingAsync(new IPEndPoint(address, port), 250);

            if (pong.Status == PongStatusEnum.OK)
            {
                _logger.LogInformation("Found Java Server within {Network}; Address = {Address}, Port = {Port}", assignment.Network, pong.Endpoint.Address.ToString(), pong.Endpoint.Port);
            }
            else if (pong.Status == PongStatusEnum.Exception)
            {
                _logger.LogError(pong.Exception, "Unknown error within {Network}; Address = {Address}, Port = {Port}", assignment.Network, pong.Endpoint.Address.ToString(), pong.Endpoint.Port);
            }

            return pong;
        }
    }
}
