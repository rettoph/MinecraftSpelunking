using Microsoft.Extensions.DependencyInjection;
using MinecraftSpelunking.Application.Account.Services;
using MinecraftSpelunking.Application.Account.Services.Implementations;

namespace MinecraftSpelunking.Application.Extensions.Microsoft.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            return services.AddScoped<IAccountApplicationService, AccountApplicationService>();
        }
    }
}
