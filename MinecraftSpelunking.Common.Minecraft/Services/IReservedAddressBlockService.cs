using System.Net;

namespace MinecraftSpelunking.Common.Minecraft.Services
{
    public interface IReservedAddressBlockService
    {
        bool IsReserved(IPNetwork2 network);
    }
}
