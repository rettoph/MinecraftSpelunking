using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftPinger.Library.Models;
using MinecraftPinger.Library.Utilities;
using MinecraftSpelunking.Common.Database;
using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Common.Minecraft.Database
{
    internal sealed class JavaServerConfiguration : BaseEntityTypeConfiguration<JavaServer>
    {
        public override void Configure(EntityTypeBuilder<JavaServer> builder)
        {
            base.Configure(builder);

            builder.HasAlternateKey(x => new { x.Host, x.Port });

            builder.Property(x => x.PlayersSample)
                .HasConversion(
                    x => MinecraftJsonSerializer.Serialize(x),
                    x => MinecraftJsonSerializer.Deserialize<Player[]>(x));

            builder.Property(x => x.Description)
                .HasConversion(
                    x => MinecraftJsonSerializer.Serialize(x),
                    x => MinecraftJsonSerializer.Deserialize<Chat>(x));

            builder.HasMany(x => x.ModVersions)
                .WithMany(x => x.JavaServers);
        }
    }
}
