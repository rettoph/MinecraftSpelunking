using Microsoft.EntityFrameworkCore;

namespace MinecraftSpelunking.Domain.Database
{
    public sealed class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
