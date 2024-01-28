using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Common.Account.Database;
using MinecraftSpelunking.Common.Account.Entities;

namespace MinecraftSpelunking.Domain.Database
{
    public sealed class DataContext : IdentityDbContext<User, UserRole, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            AccountContextConfiguration.OnModelCreating(builder);

            // any guid
            //const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            //// any guid, but nothing is against to use the same one
            //const string ROLE_ID = ADMIN_ID;
            //builder.Entity<IdentityRole>().HasData(new IdentityRole
            //{
            //    Id = ROLE_ID,
            //    Name = "admin",
            //    NormalizedName = "admin"
            //});
            //
            //var hasher = new PasswordHasher<IdentityUser>();
            //builder.Entity<IdentityUser>().HasData(new IdentityUser
            //{
            //    Id = ADMIN_ID,
            //    UserName = "admin",
            //    NormalizedUserName = "admin",
            //    Email = "admin@retto.ph",
            //    NormalizedEmail = "admin@retto.ph",
            //    EmailConfirmed = true,
            //    PasswordHash = hasher.HashPassword(null, "password"),
            //    SecurityStamp = string.Empty
            //});
            //
            //builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            //{
            //    RoleId = ROLE_ID,
            //    UserId = ADMIN_ID
            //});
        }
    }
}
