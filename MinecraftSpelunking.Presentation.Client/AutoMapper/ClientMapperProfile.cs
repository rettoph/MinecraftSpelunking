using AutoMapper;
using MinecraftPinger.Library.Models;
using MinecraftSpelunking.Presentation.Client.Models;

namespace MinecraftSpelunking.Presentation.Client.AutoMapper
{
    public sealed class ClientMapperProfile : Profile
    {
        public ClientMapperProfile()
        {
            this.CreateMap<Pong<HandshakeResponse>, Server>()
                .ForMember(d => d.Host, o => o.MapFrom(s => s.Endpoint.Address.ToString()))
                .ForMember(d => d.Port, o => o.MapFrom(s => s.Endpoint.Port));
        }
    }
}
