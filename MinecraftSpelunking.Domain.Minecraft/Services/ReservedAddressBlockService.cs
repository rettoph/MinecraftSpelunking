using MinecraftSpelunking.Common.Minecraft.Entities;
using MinecraftSpelunking.Common.Minecraft.Services;
using MinecraftSpelunking.Domain.Database;
using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Services
{
    public sealed class ReservedAddressBlockService : IReservedAddressBlockService
    {
        private readonly DataContext _context;
        private ReservedAddressBlock[]? _reservedAddressBlocks;

        public ReservedAddressBlockService(DataContext context)
        {
            _context = context;
        }

        public bool IsReserved(IPNetwork2 network)
        {
            _reservedAddressBlocks ??= _context.ReservedAddressBlocks.ToArray();

            foreach (ReservedAddressBlock reserved in _reservedAddressBlocks)
            {
                if (reserved.Network.Contains(network))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
