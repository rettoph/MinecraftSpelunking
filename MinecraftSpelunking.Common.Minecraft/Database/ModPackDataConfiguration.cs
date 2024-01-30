using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Common.Database;
using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Common.Minecraft.Database
{
    public class ModPackDataConfiguration : BaseEntityTypeConfiguration<ModPackData>
    {
        public override void Configure(EntityTypeBuilder<ModPackData> builder)
        {
            base.Configure(builder);

            builder.HasAlternateKey(x => new { x.Name, x.Version });
        }
    }
}
