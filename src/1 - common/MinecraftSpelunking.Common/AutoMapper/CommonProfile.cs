using AutoMapper;

namespace MinecraftSpelunking.Common.Extensions.AutoMapper
{
    internal class CommonProfiler : Profile
    {
        public CommonProfiler()
        {
            this.CreateMap(typeof(Page<>), typeof(Page<>));
        }
    }
}
