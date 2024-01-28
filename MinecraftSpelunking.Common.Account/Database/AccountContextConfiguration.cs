using Microsoft.AspNetCore.Identity;
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

            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = (int)UserRoleTypeEnum.User,
                UserId = UserConfiguration.InitialAdminUserId
            });

            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = (int)UserRoleTypeEnum.Admin,
                UserId = UserConfiguration.InitialAdminUserId
            });
        }
    }
}
