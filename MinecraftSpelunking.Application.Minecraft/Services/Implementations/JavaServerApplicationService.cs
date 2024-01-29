using AutoMapper;
using MinecraftSpelunking.Common.Minecraft.Services;

namespace MinecraftSpelunking.Application.Minecraft.Services.Implementations
{
    public sealed class JavaServerApplicationService : IJavaServerApplicationService
    {
        private readonly IJavaServerService _javaServers;
        private readonly IMapper _mapper;

        public JavaServerApplicationService(IJavaServerService javaServers, IMapper mapper)
        {
            _javaServers = javaServers;
            _mapper = mapper;
        }
    }
}
