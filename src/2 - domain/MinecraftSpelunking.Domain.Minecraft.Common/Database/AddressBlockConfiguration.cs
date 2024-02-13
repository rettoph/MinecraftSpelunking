using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Database
{
    internal sealed class AddressBlockConfiguration : IEntityTypeConfiguration<AddressBlock>
    {
        public void Configure(EntityTypeBuilder<AddressBlock> builder)
        {
        }
    }
}
