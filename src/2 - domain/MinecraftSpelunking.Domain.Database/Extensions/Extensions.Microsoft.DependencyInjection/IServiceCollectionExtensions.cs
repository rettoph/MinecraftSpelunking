using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MinecraftSpelunking.Domain.Database.Extensions.Microsoft.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDatabaseServices(this IServiceCollection services, IConfigurationManager configuration)
        {
            // Add database
            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly("MinecraftSpelunking.Domain.Database")));

            return services;
        }
    }
}
