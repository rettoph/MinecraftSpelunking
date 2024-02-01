using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Domain.Identity.Common.Entities;

namespace MinecraftSpelunking.Domain.Database
{
    public sealed class DataContext : IdentityDbContext<User, UserRole, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
