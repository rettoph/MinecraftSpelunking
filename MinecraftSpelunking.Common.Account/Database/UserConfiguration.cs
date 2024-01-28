using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinecraftSpelunking.Common.Account.Entities;

namespace MinecraftSpelunking.Common.Account.Database
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public const int InitialAdminUserId = 1;
        public const string InitialAdminUserApiKey = "12345";
        public const string InitialAdminUserPassword = "password";

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasAlternateKey(x => x.ApiAccessToken);

            var hasher = new PasswordHasher<User>();
            builder.HasData(new User
            {
                Id = InitialAdminUserId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@retto.ph",
                NormalizedEmail = "admin@retto.ph",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, InitialAdminUserPassword),
                SecurityStamp = string.Empty,
                ApiAccessToken = InitialAdminUserApiKey
            });
        }
    }
}
