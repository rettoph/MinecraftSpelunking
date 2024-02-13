using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MinecraftSpelunking.Common.Database;
using MinecraftSpelunking.Domain.Database.Common;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Database.Converters;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;

namespace MinecraftSpelunking.Domain.Database
{
    public sealed class DataContext : IdentityDbContext<User, UserRole, int>, IDataContext
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

            builder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(Server).Assembly);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            IpNetwork2Converter.ConfigureConventions(configurationBuilder);
        }

        private void PrepareEntitiesForSave()
        {
            DateTime now = DateTime.Now;
            IEnumerable<EntityEntry> entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (EntityEntry entityEntry in entries)
            {
                if (entityEntry.Entity is BaseEntity entity)
                {
                    entity.ModifiedAt = now;

                    if (entityEntry.State == EntityState.Added)
                    {
                        entity.CreatedAt = now;
                    }
                }
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.PrepareEntitiesForSave();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.PrepareEntitiesForSave();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
