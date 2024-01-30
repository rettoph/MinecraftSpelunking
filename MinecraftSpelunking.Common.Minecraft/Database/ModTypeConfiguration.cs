using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Common.Database;
using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Common.Minecraft.Database
{
    internal class ModTypeConfiguration : BaseEntityTypeConfiguration<ModType>
    {
        public override void Configure(EntityTypeBuilder<ModType> builder)
        {
            base.Configure(builder);

            builder.HasAlternateKey(x => x.Name);
        }
    }
}
