using Microsoft.Extensions.DependencyInjection;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Identity.Common.Entities;

namespace MinecraftSpelunking.Domain.Identity.Extensions.Microsoft.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<User, UserRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
            }).AddEntityFrameworkStores<DataContext>();

            return services;
        }
    }
}
