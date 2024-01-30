using AutoMapper;
using MinecraftSpelunker.Application.Common.Dtos;
using MinecraftSpelunking.Application.Minecraft.Dtos;
using MinecraftSpelunking.Presentation.WebServer.Models;

namespace MinecraftSpelunking.Presentation.WebServer.AutoMapper
{
    public class WebServerMapperProfile : Profile
    {
        public WebServerMapperProfile()
        {
            this.CreateMap(typeof(PageDto<>), typeof(PageModel<>));

            this.CreateMap<AddressBlockDto, AddressBlockModel>();

            this.CreateMap<ServerModel, ServerDto>();

            this.CreateMap<JavaServerDto, JavaServerModel>();
            this.CreateMap<ServerIconDto, ServerIconModel>();
            this.CreateMap<ChatDto, ChatModel>();
            this.CreateMap<PlayerDto, PlayerModel>();

            this.CreateMap<ModDto, ModModel>();
            this.CreateMap<ModPackDataDto, ModPackDataModel>();
            this.CreateMap<ModTypeDto, ModTypeModel>();
            this.CreateMap<ModVersionDto, ModVersionModel>();
        }
    }
}
