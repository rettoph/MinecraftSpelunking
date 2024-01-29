using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Common.Database;
using MinecraftSpelunking.Common.Minecraft.Entities;
using System.Net;

namespace MinecraftSpelunking.Common.Minecraft.Database
{
    internal sealed class AddressBlockConfiguration : BaseEntityTypeConfiguration<AddressBlock>
    {
        public override void Configure(EntityTypeBuilder<AddressBlock> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Network)
                .HasConversion(
                    v => v.ToString(),
                    v => IPNetwork2.Parse(v));
        }
    }
}
