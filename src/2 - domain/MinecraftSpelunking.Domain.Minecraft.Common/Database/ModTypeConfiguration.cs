using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Database
{
    internal class ModTypeConfiguration : IEntityTypeConfiguration<ModType>
    {
        public void Configure(EntityTypeBuilder<ModType> builder)
        {
            builder.HasAlternateKey(x => x.Name);
        }
    }
}
