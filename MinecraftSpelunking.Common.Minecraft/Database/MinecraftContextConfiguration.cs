using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Common.Database;

namespace MinecraftSpelunking.Common.Minecraft.Database
{
    public class MinecraftContextConfiguration : IDataContextConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ReservedAddressBlockConfiguration());
            builder.ApplyConfiguration(new AddressBlockConfiguration());
            builder.ApplyConfiguration(new JavaServerConfiguration());
            builder.ApplyConfiguration(new ModTypeConfiguration());
            builder.ApplyConfiguration(new ModConfiguration());
            builder.ApplyConfiguration(new ModVersionConfiguration());
            builder.ApplyConfiguration(new ModPackDataConfiguration());
        }
    }
}
