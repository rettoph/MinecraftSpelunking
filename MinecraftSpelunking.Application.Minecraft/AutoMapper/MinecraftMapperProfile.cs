using AutoMapper;
using MinecraftPinger.Library.Models;
using MinecraftSpelunking.Application.Minecraft.Dtos;
using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Application.Minecraft
{
    public class MinecraftMapperProfile : Profile
    {
        public MinecraftMapperProfile()
        {
            this.CreateMap<AddressBlock, AddressBlockDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Network, o => o.MapFrom(s => s.Network.ToString()));

            this.CreateMap<ServerIcon, ServerIconDto>();

            this.CreateMap<Server, ServerDto>();
            this.CreateMap<ServerDto, Server>();

            this.CreateMap<JavaServer, JavaServerDto>();

            this.CreateMap<Chat, ChatDto>();

            this.CreateMap<Player, PlayerDto>();

            this.CreateMap<Player, PlayerDto>();
        }
    }
}
