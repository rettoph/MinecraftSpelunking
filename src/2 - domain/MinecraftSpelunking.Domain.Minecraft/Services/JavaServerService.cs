using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinecraftPinger.Library;
using MinecraftPinger.Library.Enums;
using MinecraftPinger.Library.Models;
using MinecraftSpelunking.Domain.Common.Services;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Enums;
using MinecraftSpelunking.Domain.Minecraft.Common.Services;
using Mod = MinecraftSpelunking.Domain.Minecraft.Common.Entities.Mod;
using ModPackData = MinecraftSpelunking.Domain.Minecraft.Common.Entities.ModPackData;

namespace MinecraftSpelunking.Domain.Minecraft.Services
{
    public class JavaServerService : BaseEntityService<JavaServer>, IJavaServerService
    {
        private readonly JavaPinger _pinger;
        private readonly IServerIconService _serverIcons;

        public JavaServerService(JavaPinger pinger, IServerIconService icons, DataContext context, IMapper mapper) : base(x => x.JavaServers, context, mapper)
        {
            _pinger = pinger;
            _serverIcons = icons;
        }

        public async Task<JavaServer> AddAsync(Server info, AddressBlock block)
        {
            JavaServer? server = await this.entities.Include(x => x.Icon).Include(x => x.ModVersions)
                .FirstOrDefaultAsync(x => x.Host == info.Host && x.Port == info.Port);

            if (server is null)
            {
                server = this.entities.Add(new JavaServer()
                {
                    Host = info.Host,
                    Port = info.Port,
                    Status = ServerStatusEnum.Offline
                }).Entity;
            }

            server.AddressBlock = block;

            await this.RefreshAsync(server);

            return server;

        }

        public async Task<JavaServer?> RefreshAsync(Server info)
        {
            JavaServer? server = await this.entities.Include(x => x.Icon).Include(x => x.ModVersions)
                .FirstOrDefaultAsync(x => x.Host == info.Host && x.Port == info.Port);

            if (server is not null)
            {
                await this.RefreshAsync(server);
            }

            return server;
        }

        private async Task RefreshAsync(JavaServer server)
        {
            Pong<HandshakeResponse> pong = await _pinger.PingAsync(server.Host, server.Port);

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

            ModType info = await this.context.ModTypes.FirstOrDefaultAsync(x => x.Name == modInfo.Type) ?? this.context.ModTypes.Add(new ModType()
            {
                Name = modInfo.Type
            }).Entity; ;

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

            ModPackData data = await this.context.ModPackData.FirstOrDefaultAsync(x => x.Name == modPackData.Name && x.Version == modPackData.Version) ?? this.context.ModPackData.Add(new ModPackData()
            {
                ProjectId = modPackData.ProjectId,
                Name = modPackData.Name,
                Version = modPackData.Version,
                VersionId = modPackData.VersionId,
                IsMetadata = modPackData.IsMetadata,
            }).Entity;

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
                Mod? mod = await this.context.Mods.FirstOrDefaultAsync(x => x.Name == handshakeMod.ModId);
                if (mod is null)
                {
                    mod = new Mod()
                    {
                        Name = handshakeMod.ModId
                    };

                    this.context.Add(mod);

                    version = new ModVersion()
                    {
                        Mod = mod,
                        Version = handshakeMod.Version
                    };

                    this.context.Add(version);
                }
                else
                {
                    version = await this.context.ModVersions.FirstOrDefaultAsync(x => x.ModId == mod.Id && x.Version == handshakeMod.Version);

                    if (version is null)
                    {
                        version = new ModVersion()
                        {
                            Mod = mod,
                            Version = handshakeMod.Version
                        };

                        this.context.Add(version);
                    }
                }

                modVersions.Add(version);
            }

            return modVersions.ToArray();
        }
    }
}
