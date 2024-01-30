using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Common.Database;
using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Common.Minecraft.Database
{
    internal class ModVersionConfiguration : BaseEntityTypeConfiguration<ModVersion>
    {
        public override void Configure(EntityTypeBuilder<ModVersion> builder)
        {
            base.Configure(builder);

            builder.HasAlternateKey(x => new { x.ModId, x.Version });
        }
    }
}
