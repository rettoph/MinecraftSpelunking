using MinecraftSpelunking.Common.Account.Entities;
using MinecraftSpelunking.Common.Minecraft.Entities;
using MinecraftSpelunking.Common.Minecraft.Enums;
using MinecraftSpelunking.Common.Minecraft.Services;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Minecraft.Extensions.System.Net;
using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Services
{
    public class AddressBlockService : IAddressBlockService
    {
        private readonly TimeSpan ReassignInterval = TimeSpan.FromMinutes(10);

        private readonly IReservedAddressBlockService _reservedAddressBlocks;
        private readonly IJavaServerService _javaServers;
        private readonly DataContext _context;

        public AddressBlockService(
            IReservedAddressBlockService reservedAddressBlocks,
            IJavaServerService javaServers,
            DataContext context)
        {
            _context = context;
            _reservedAddressBlocks = reservedAddressBlocks;
            _javaServers = javaServers;
        }

        public AddressBlock GetById(int id)
        {
            return _context.AddressBlocks.First(x => x.Id == id);
        }

        public void Complete(int id, Server[] javaDiscoveries)
        {
            AddressBlock block = this.GetById(id);

            foreach (Server javaDiscovery in javaDiscoveries)
            {
                _javaServers.Add(javaDiscovery.Host, javaDiscovery.Port, block);
            }

            block.Status = AddressBlockStatusEnum.Scanned;
            block.ModifiedAt = DateTime.Now;

            _context.SaveChanges();
        }

        public AddressBlock GetAssignment(User user)
        {
            DateTime reassignTime = DateTime.Now.Subtract(ReassignInterval);
            AddressBlock? old = _context.AddressBlocks
                .Where(x => x.Status == AddressBlockStatusEnum.Assigned)
                .Where(x => reassignTime > x.ModifiedAt)
                .FirstOrDefault();

            if (old is not null)
            {
                this.Assign(old, user);
                _context.SaveChanges();

                return old;
            }

            AddressBlock? last = _context.AddressBlocks.OrderByDescending(x => x.Id).FirstOrDefault();
            IPNetwork2 network = default!;
            if (last is null)
            {
                network = IPNetwork2.Parse($"0.0.0.0/{AddressBlock.CIDR}");
            }
            else
            {
                network = last.Network.CalculateNextNetwork();
            }

            AddressBlock next = default!;
            do
            {
                next = new AddressBlock()
                {
                    Network = network,
                    Status = _reservedAddressBlocks.IsReserved(network) ? AddressBlockStatusEnum.Reserved : AddressBlockStatusEnum.Available,
                    ModifiedAt = DateTime.Now
                };

                _context.Add(next);

                if (next.Status == AddressBlockStatusEnum.Reserved)
                {
                    network = network.CalculateNextNetwork();
                }
                else if (next.Status == AddressBlockStatusEnum.Available)
                {
                    next.Status = AddressBlockStatusEnum.Assigned;
                    this.Assign(next, user);
                }
            } while (next.Status != AddressBlockStatusEnum.Assigned);

            _context.SaveChanges();

            return next;
        }

        private void Assign(AddressBlock block, User user)
        {
            block.Status = AddressBlockStatusEnum.Assigned;
            block.ModifiedAt = DateTime.Now;

            AddressBlockAssignment assignment = new AddressBlockAssignment()
            {
                User = user,
                Block = block,
                AssignedAt = DateTime.Now
            };

            _context.Add(assignment);
        }
    }
}
