using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Identity.Common.Services;
using MinecraftSpelunking.Domain.Identity.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<User, UserRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<DataContext>();

            return services.AddScoped<IUserService, UserService>();
        }
    }
}
