using MinecraftSpelunking.Application.Identity.Common.AutoMapper;
using MinecraftSpelunking.Application.Identity.Common.Services;
using MinecraftSpelunking.Application.Identity.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationIdentityServices(this IServiceCollection services)
        {
            return services.RegisterDomainIdentityServices()
                .AddAutoMapper(typeof(ApplicationIdentityMapperProfile))
                .AddScoped<IUserApplicationService, UserApplicationService>();
        }
    }
}
