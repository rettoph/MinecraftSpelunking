using AutoMapper;
using MinecraftSpelunking.Application.Minecraft.Dtos;
using MinecraftSpelunking.Presentation.WebServer.Models;

namespace MinecraftSpelunking.Presentation.WebServer.AutoMapper
{
    public class WebServerMapperProfile : Profile
    {
        public WebServerMapperProfile()
        {
            this.CreateMap<ServerModel, ServerDto>();
        }
    }
}
