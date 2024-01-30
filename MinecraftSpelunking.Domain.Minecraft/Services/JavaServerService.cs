using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinecraftPinger.Library;
using MinecraftPinger.Library.Enums;
using MinecraftPinger.Library.Models;
using MinecraftSpelunking.Common.Minecraft.Entities;
using MinecraftSpelunking.Common.Minecraft.Enums;
using MinecraftSpelunking.Common.Minecraft.Services;
using MinecraftSpelunking.Domain.Database;
using Mod = MinecraftSpelunking.Common.Minecraft.Entities.Mod;
using ModPackData = MinecraftSpelunking.Common.Minecraft.Entities.ModPackData;

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

        public async Task<JavaServer> GetByIdAsync(int id)
        {
            return await _context.JavaServers
                .Include(x => x.Icon)
                .Include(x => x.ModType)
                .Include(x => x.ModPackData)
                .Include(x => x.ModVersions)
                .ThenInclude(x => x.Mod)
                .FirstAsync(x => x.Id == id);
        }

        public Task<JavaServer[]> GetAllAsync(int count, int offset)
        {
            JavaServer[] servers = _context.JavaServers
                .Include(x => x.Icon)
                .Include(x => x.ModType)
                .Include(x => x.ModPackData)
                .Include(x => x.ModVersions)
                .ThenInclude(x => x.Mod)
                .OrderBy(x => x.Id)
                .Skip(offset)
                .Take(count)
                .ToArray();

            return Task.FromResult(servers);
        }

        public async Task<JavaServer> AddAsync(string host, int port, AddressBlock block)
        {
            JavaServer? server = await _context.JavaServers
                .Include(x => x.Icon)
                .Include(x => x.ModVersions)
                .FirstOrDefaultAsync(x => x.Host == host && x.Port == port);

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

            Pong<HandshakeResponse> pong = await _pinger.PingAsync(host, port);
            await this.UpdateServerAsync(pong, server);
            _context.SaveChanges();

            return server;
        }

        public async Task<JavaServer> RefreshAsync(int id)
        {
            JavaServer server = _context.JavaServers
                .Include(x => x.Icon)
                .Include(x => x.ModVersions)
                .First(x => x.Id == id);

            Pong<HandshakeResponse> pong = await _pinger.PingAsync(server.Host, server.Port);
            await this.UpdateServerAsync(pong, server);
            _context.SaveChanges();

            return server;
        }

        private async Task UpdateServerAsync(Pong<HandshakeResponse> pong, JavaServer server)
        {
            if (pong.Status == PongStatusEnum.OK && pong.Content is not null)
            {
                server.Status = ServerStatusEnum.Online;
                server.Version = pong.Content.Version.Name;
                server.PlayersMax = pong.Content.Players.Max;
                server.PlayersOnline = pong.Content.Players.Online;
                server.PlayersSample = pong.Content.Players.Sample;
                server.LastOnlineAt = DateTime.Now;

                server.ModPackData = await this.GetModPackDataAsync(pong.Content.ModPackData, server.ModPackData);
                server.ModType = await this.GetModTypeAsync(pong.Content.ModInfo, server.ModType);
                server.ModVersions.AddRange(await this.GetModVersions(pong.Content.ModInfo));

                server.Description = pong.Content.Description;
                server.DescriptionNormalized = pong.Content.Description.ToString();

                if (pong.Content.Icon.IsNullOrEmpty() == false
                    && (server.Icon is null || server.Icon.Hash != _serverIcons.GetBase64Hash(pong.Content.Icon)))
                {
                    server.Icon = _serverIcons.Add(pong.Content.Icon);
                }
            }
            else
            {
                if (server.Status == ServerStatusEnum.Online)
                {
                    server.LastOnlineAt = server.ModifiedAt;
                }

                server.Status = pong.Status switch
                {
                    PongStatusEnum.Timeout => ServerStatusEnum.Offline,
                    _ => ServerStatusEnum.Unknown
                };
            }

            server.ModifiedAt = DateTime.Now;
        }

        private async Task<ModType?> GetModTypeAsync(ModInfo? modInfo, ModType? oldType)
        {
            if (modInfo is null)
            {
                return null;
            }

            if (oldType?.Name == modInfo.Type)
            {
                return oldType;
            }

            ModType? info = await _context.ModTypes.FirstOrDefaultAsync(x => x.Name == modInfo.Type);

            if (info is null)
            {
                info = new ModType()
                {
                    Name = modInfo.Type
                };

                _context.Add(info);
            }

            return info;
        }

        private async Task<ModPackData?> GetModPackDataAsync(MinecraftPinger.Library.Models.ModPackData? modPackData, ModPackData? oldModPackData)
        {
            if (modPackData is null)
            {
                return null;
            }

            if (oldModPackData?.ProjectId == modPackData.ProjectId && oldModPackData?.VersionId == modPackData.VersionId)
            {
                return oldModPackData;
            }

            ModPackData? data = await _context.ModPackData.FirstOrDefaultAsync(x => x.Name == modPackData.Name && x.Version == modPackData.Version);

            if (data is null)
            {
                data = new ModPackData()
                {
                    ProjectId = modPackData.ProjectId,
                    Name = modPackData.Name,
                    Version = modPackData.Version,
                    VersionId = modPackData.VersionId,
                    IsMetadata = modPackData.IsMetadata,
                };

                _context.Add(data);
            }

            return data;
        }

        private async Task<ModVersion[]> GetModVersions(ModInfo? modInfo)
        {
            if (modInfo is null)
            {
                return Array.Empty<ModVersion>();
            }

            List<ModVersion> modVersions = new List<ModVersion>();

            foreach (var handshakeMod in modInfo.List)
            {
                ModVersion? version;
                Mod? mod = await _context.Mods.FirstOrDefaultAsync(x => x.Name == handshakeMod.ModId);
                if (mod is null)
                {
                    mod = new Mod()
                    {
                        Name = handshakeMod.ModId
                    };

                    _context.Add(mod);

                    version = new ModVersion()
                    {
                        Mod = mod,
                        Version = handshakeMod.Version
                    };

                    _context.Add(version);
                }
                else
                {
                    version = await _context.ModVersions.FirstOrDefaultAsync(x => x.ModId == mod.Id && x.Version == handshakeMod.Version);

                    if (version is null)
                    {
                        version = new ModVersion()
                        {
                            Mod = mod,
                            Version = handshakeMod.Version
                        };

                        _context.Add(version);
                    }
                }

                modVersions.Add(version);
            }

            return modVersions.ToArray();
        }
    }
}