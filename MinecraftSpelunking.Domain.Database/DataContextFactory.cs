using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MinecraftSpelunking.Domain.Database
{
    public sealed class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<DataContext>();

            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=MinecraftSpelunking;Trusted_Connection=True;MultipleActiveResultSets=true";

            dbContextBuilder.UseSqlServer(connectionString);

            return new DataContext(dbContextBuilder.Options);
        }
    }
}
