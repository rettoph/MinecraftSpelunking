using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Database
{
    internal class ModPackDataConfiguration : IEntityTypeConfiguration<ModPackData>
    {
        public void Configure(EntityTypeBuilder<ModPackData> builder)
        {
            builder.HasAlternateKey(x => new { x.Name, x.Version });
        }
    }
}
