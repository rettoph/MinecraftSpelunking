using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinecraftSpelunking.Common.Account.Entities;
using MinecraftSpelunking.Common.Account.Services;
using MinecraftSpelunking.Domain.Account.Services;
using MinecraftSpelunking.Domain.Database;

namespace MinecraftSpelunking.Domain.Extensions.Microsoft.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services, IConfigurationManager configuration)
        {
            // Add database
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(connectionString, x => x.MigrationsAssembly("MinecraftSpelunking.Domain.Database")));

            services.AddIdentity<User, UserRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
            }).AddEntityFrameworkStores<DataContext>();

            return services.AddScoped<IAccountService, AccountService>();
        }
    }
}
