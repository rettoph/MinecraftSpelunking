using AutoMapper.EntityFrameworkCore;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Application.Database.Common.Services
{
    public static class IMapperApplicationServiceExtensions
    {
        public static IPersistence<AddressBlock> AddressBlocks(this IMapperApplicationService mapper)
        {
            return mapper.Persist(x => x.AddressBlocks);
        }
    }
}
