using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Common.Database;
using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Common.Minecraft.Database
{
    public class ModConfiguration : BaseEntityTypeConfiguration<Mod>
    {
        public override void Configure(EntityTypeBuilder<Mod> builder)
        {
            base.Configure(builder);

            builder.HasAlternateKey(x => x.Name);
        }
    }
}
