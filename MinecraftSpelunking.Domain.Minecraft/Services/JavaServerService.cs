using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinecraftPinger.Library;
using MinecraftPinger.Library.Models;
using MinecraftSpelunking.Common.Minecraft.Entities;
using MinecraftSpelunking.Common.Minecraft.Enums;
using MinecraftSpelunking.Common.Minecraft.Services;
using MinecraftSpelunking.Domain.Database;

namespace MinecraftSpelunking.Domain.Minecraft.Services
{
    public sealed class JavaServerService : IJavaServerService
    {
        private readonly JavaPinger _pinger;
        private readonly DataContext _context;
        private readonly IServerIconService _serverIcons;

        public JavaServerService(
            DataContext context,
            JavaPinger pinger,
            IServerIconService serverIcons)
        {
            _context = context;
            _pinger = pinger;
            _serverIcons = serverIcons;
        }

        public JavaServer Add(string host, int port, AddressBlock block)
        {
            JavaServer? server = _context.JavaServers
                .Include(x => x.Icon)
                .FirstOrDefault(x => x.Host == host && x.Port == port);

            if (server is null)
            {
                server = new JavaServer()
                {
                    Host = host,
                    Port = port,
                    Status = ServerStatusEnum.Offline
                };

                _context.Add(server);
            }

            server.AddressBlock = block;

            HandshakeResponse? handshake = _pinger.Ping(host, port);
            this.SyncServer(handshake, server);
            _context.SaveChanges();

            return server;
        }

        private void SyncServer(HandshakeResponse? handshake, JavaServer server)
        {
            if (handshake is null)
            {
                if (server.Status == ServerStatusEnum.Online)
                {
                    server.LastOnlineAt = server.ModifiedAt;
                }

                server.Status = ServerStatusEnum.Offline;
            }
            else
            {
                server.PlayersMax = handshake.Players.Max;
                server.PlayersOnline = handshake.Players.Online;
                server.PlayersSample = handshake.Players.Sample;

                server.Description = handshake.Description;

                if (handshake.Icon.IsNullOrEmpty() == false
                    && (server.Icon is null || server.Icon.Hash != _serverIcons.GetBase64Hash(handshake.Icon)))
                {
                    server.Icon = _serverIcons.Add(handshake.Icon);
                }
            }

            server.ModifiedAt = DateTime.Now;
        }
    }
}
