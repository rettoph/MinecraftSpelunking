using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Database
{
    internal class ModVersionConfiguration : IEntityTypeConfiguration<ModVersion>
    {
        public void Configure(EntityTypeBuilder<ModVersion> builder)
        {
            builder.HasAlternateKey(x => new { x.ModId, x.Version });
        }
    }
}
