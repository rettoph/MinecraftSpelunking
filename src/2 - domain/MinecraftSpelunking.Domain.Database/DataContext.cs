using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Domain.Database.Common;
using MinecraftSpelunking.Domain.Identity.Common.Entities;

namespace MinecraftSpelunking.Domain.Database
{
    public sealed class DataContext : IdentityDbContext<User, UserRole, int>, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);
        }
    }
}
