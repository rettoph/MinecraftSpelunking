using AutoMapper;
using MinecraftSpelunking.Application.Identity.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Common.Dtos;
using MinecraftSpelunking.Presentation.Common.Models;
using System.Net;

namespace MinecraftSpelunking.Presentation.WebServer.AutoMapper
{
    internal sealed class PresentationCommonProfile : Profile
    {
        public PresentationCommonProfile()
        {
            this.CreateMap<ServerModel, ServerDto>();

            this.CreateMap<UserDto, UserModel>();

            this.CreateMap<AddressBlockAssignmentDto, AddressBlockAssignmentModel>();

            this.CreateMap<AddressBlockDto, AddressBlockModel>();

            this.CreateMap<IPNetwork2, IPNetwork2Model>()
                .ForMember(s => s.Network, o => o.MapFrom(d => d.Network.ToString()))
                .ForMember(s => s.Cidr, o => o.MapFrom(d => d.Cidr));
        }
    }
}
