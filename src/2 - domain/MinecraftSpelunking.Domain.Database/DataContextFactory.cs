using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MinecraftSpelunking.Domain.Database
{
    public sealed class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<DataContext> dbContextBuilder = new DbContextOptionsBuilder<DataContext>();

            string connectionString = args.Length switch
            {
                0 => Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ?? throw new ArgumentNullException("ConnectionStrings__DefaultConnection"),
                _ => args[0]
            };

            dbContextBuilder.UseSqlServer(connectionString);

            return new DataContext(dbContextBuilder.Options);
        }
    }
}
