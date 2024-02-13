using MinecraftSpelunking.Common.Services;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Services
{
    public interface IReservedAddressBlockService : IBaseEntityService<ReservedAddressBlock>
    {
        bool IsReserved(IPNetwork2 network);
    }
}
