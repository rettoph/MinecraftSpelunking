using AutoMapper;
using MinecraftSpelunking.Application.Identity.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Common.Dtos;
using MinecraftSpelunking.Presentation.WebServer.Models;
using System.Net;

namespace MinecraftSpelunking.Presentation.WebServer.AutoMapper
{
    internal sealed class WebServerProfile : Profile
    {
        public WebServerProfile()
        {
            this.CreateMap<UserDto, UserModel>();

            this.CreateMap<AddressBlockAssignmentDto, AddressBlockAssignmentModel>();
            this.CreateMap<AddressBlockDto, AddressBlockModel>();
            this.CreateMap<IPNetwork2, IPNetwork2Model>()
                .ForMember(s => s.Network, o => o.MapFrom(d => d.Network.ToString()))
                .ForMember(s => s.Cidr, o => o.MapFrom(d => d.Cidr));
        }
    }
}
