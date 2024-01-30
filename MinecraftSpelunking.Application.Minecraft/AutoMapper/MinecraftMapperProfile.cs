using AutoMapper;
using MinecraftPinger.Library.Models;
using MinecraftSpelunking.Application.Minecraft.Dtos;
using MinecraftSpelunking.Common.Minecraft.Entities;
using System.Net;
using Mod = MinecraftSpelunking.Common.Minecraft.Entities.Mod;
using ModPackData = MinecraftSpelunking.Common.Minecraft.Entities.ModPackData;

namespace MinecraftSpelunking.Application.Minecraft
{
    public class MinecraftMapperProfile : Profile
    {
        public MinecraftMapperProfile()
        {
            this.CreateMap<AddressBlock, AddressBlockDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Network, o => o.MapFrom(s => s.Network.ToString()));

            this.CreateMap<AddressBlockDto, AddressBlock>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Network, o => o.MapFrom(s => IPNetwork2.Parse(s.Network)));

            this.CreateMap<ServerIcon, ServerIconDto>();

            this.CreateMap<Server, ServerDto>();
            this.CreateMap<ServerDto, Server>();

            this.CreateMap<JavaServer, JavaServerDto>();

            this.CreateMap<Chat, ChatDto>();

            this.CreateMap<Player, PlayerDto>();

            this.CreateMap<Player, PlayerDto>();

            this.CreateMap<Mod, ModDto>();
            this.CreateMap<ModPackData, ModPackDataDto>();
            this.CreateMap<ModType, ModTypeDto>();
            this.CreateMap<ModVersion, ModVersionDto>();
        }
    }
}
