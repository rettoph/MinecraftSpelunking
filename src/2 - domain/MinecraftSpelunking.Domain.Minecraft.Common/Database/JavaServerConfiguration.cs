using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftPinger.Library.Models;
using MinecraftPinger.Library.Utilities;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Minecraft.Common.Database
{
    internal class JavaServerConfiguration : IEntityTypeConfiguration<JavaServer>
    {
        public void Configure(EntityTypeBuilder<JavaServer> builder)
        {
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
