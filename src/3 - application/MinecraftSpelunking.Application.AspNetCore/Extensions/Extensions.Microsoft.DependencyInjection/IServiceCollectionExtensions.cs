using Microsoft.Extensions.DependencyInjection;
using MinecraftSpelunking.Application.AspNetCore.Common.Services;
using MinecraftSpelunking.Application.AspNetCore.Services;

namespace MinecraftSpelunking.Application.AspNetCore.Extensions.Microsoft.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAspNetCoreServices(this IServiceCollection services)
        {
            return services.AddHttpContextAccessor()
                .AddScoped<ICookieApplicationService, CookieApplicationService>()
                .AddScoped<IRedirectApplicationService, RedirectApplicationService>()
                .AddScoped<IStatusMessageApplicationService, StatusMessageApplicationService>();
        }
    }
}
