using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Database
{
    internal class ModConfiguration : IEntityTypeConfiguration<Mod>
    {
        public void Configure(EntityTypeBuilder<Mod> builder)
        {
            builder.HasAlternateKey(x => x.Name);
        }
    }
}
