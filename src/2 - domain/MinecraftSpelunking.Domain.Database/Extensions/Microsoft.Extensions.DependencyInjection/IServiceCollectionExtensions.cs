using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Database.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainDatabaseServices(this IServiceCollection services, IConfigurationManager configuration)
        {
            // Add database
            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<IDataContext, DataContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly("MinecraftSpelunking.Domain.Database")));

            return services;
        }
    }
}
