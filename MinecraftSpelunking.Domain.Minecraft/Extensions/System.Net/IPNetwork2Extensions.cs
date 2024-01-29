using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Extensions.System.Net
{
    public static class IPNetwork2Extensions
    {
        public static IPNetwork2 CalculateNextNetwork(this IPNetwork2 network)
        {
            IPAddress next = network.Network.IncrementIpAddress((uint)network.Total);

            return new IPNetwork2(next, network.Cidr);
        }
    }
}
