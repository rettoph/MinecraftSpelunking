using AutoMapper;
using MinecraftSpelunking.Domain.Common.Services;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Services;
using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Services
{
    internal sealed class ReservedAddressBlockService : BaseEntityService<ReservedAddressBlock>, IReservedAddressBlockService
    {
        private ReservedAddressBlock[]? _reservedAddressBlocks;

        public ReservedAddressBlockService(DataContext context, IMapper mapper) : base(x => x.ReservedAddressBlocks, context, mapper)
        {
        }

        public bool IsReserved(IPNetwork2 network)
        {
            _reservedAddressBlocks ??= this.entities.ToArray();

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
