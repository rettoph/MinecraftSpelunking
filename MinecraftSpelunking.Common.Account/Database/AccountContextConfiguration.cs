using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Common.Database;

namespace MinecraftSpelunking.Common.Account.Database
{
    public class AccountContextConfiguration : IDataContextConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
