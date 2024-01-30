using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Common.Account.Database;
using MinecraftSpelunking.Common.Account.Entities;
using MinecraftSpelunking.Common.Minecraft.Database;
using MinecraftSpelunking.Common.Minecraft.Entities;

namespace MinecraftSpelunking.Domain.Database
{
    public sealed class DataContext : IdentityDbContext<User, UserRole, int>
    {
        public DbSet<ReservedAddressBlock> ReservedAddressBlocks { get; set; }
        public DbSet<AddressBlock> AddressBlocks { get; set; }
        public DbSet<AddressBlockAssignment> AddressBlockAssignments { get; set; }
        public DbSet<JavaServer> JavaServers { get; set; }
        public DbSet<ServerIcon> ServerIcons { get; set; }
        public DbSet<ModType> ModTypes { get; set; }
        public DbSet<Mod> Mods { get; set; }
        public DbSet<ModVersion> ModVersions { get; set; }
        public DbSet<ModPackData> ModPackData { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            AccountContextConfiguration.OnModelCreating(builder);
            MinecraftContextConfiguration.OnModelCreating(builder);

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
