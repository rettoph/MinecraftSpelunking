using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using System.Net;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Database
{
    internal class ReservedAddressBlockConfiguration : IEntityTypeConfiguration<ReservedAddressBlock>
    {
        public void Configure(EntityTypeBuilder<ReservedAddressBlock> builder)
        {
            builder.HasData(
                new ReservedAddressBlock()
                {
                    Id = 1,
                    Network = IPNetwork2.Parse("0.0.0.0/8")
                },
                new ReservedAddressBlock()
                {
                    Id = 2,
                    Network = IPNetwork2.Parse("10.0.0.0/8")
                },
                new ReservedAddressBlock()
                {
                    Id = 3,
                    Network = IPNetwork2.Parse("100.64.0.0/10")
                },
                new ReservedAddressBlock()
                {
                    Id = 4,
                    Network = IPNetwork2.Parse("127.0.0.0/8")
                },
                new ReservedAddressBlock()
                {
                    Id = 5,
                    Network = IPNetwork2.Parse("169.254.0.0/16")
                },
                new ReservedAddressBlock()
                {
                    Id = 6,
                    Network = IPNetwork2.Parse("172.16.0.0/12")
                },
                new ReservedAddressBlock()
                {
                    Id = 7,
                    Network = IPNetwork2.Parse("192.0.0.0/24")
                },
                new ReservedAddressBlock()
                {
                    Id = 8,
                    Network = IPNetwork2.Parse("192.0.2.0/24")
                },
                new ReservedAddressBlock()
                {
                    Id = 9,
                    Network = IPNetwork2.Parse("192.88.99.0/24")
                },
                new ReservedAddressBlock()
                {
                    Id = 10,
                    Network = IPNetwork2.Parse("192.168.0.0/16")
                },
                new ReservedAddressBlock()
                {
                    Id = 11,
                    Network = IPNetwork2.Parse("198.18.0.0/15")
                },
                new ReservedAddressBlock()
                {
                    Id = 12,
                    Network = IPNetwork2.Parse("198.51.100.0/24")
                },
                new ReservedAddressBlock()
                {
                    Id = 13,
                    Network = IPNetwork2.Parse("203.0.113.0/24")
                },
                new ReservedAddressBlock()
                {
                    Id = 14,
                    Network = IPNetwork2.Parse("224.0.0.0/4")
                },
                new ReservedAddressBlock()
                {
                    Id = 15,
                    Network = IPNetwork2.Parse("240.0.0.0/4")
                },
                new ReservedAddressBlock()
                {
                    Id = 16,
                    Network = IPNetwork2.Parse("255.255.255.255/32")
                });
        }
    }
}
