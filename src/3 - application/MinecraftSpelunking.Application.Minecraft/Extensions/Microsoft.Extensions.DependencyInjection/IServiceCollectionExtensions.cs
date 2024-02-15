using MinecraftSpelunking.Application.Minecraft.Common.AutoMapper;
using MinecraftSpelunking.Application.Minecraft.Common.Services;
using MinecraftSpelunking.Application.Minecraft.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationMinecraftServices(this IServiceCollection services)
        {
            return services.RegisterDomainMinecraftServices()
                .AddAutoMapper(typeof(ApplicationMinecraftMapperProfile))
                .AddScoped<IAddressBlockAssignmentApplicationService, AddressBlockAssignmentApplicationService>()
                .AddScoped<IAddressBlockApplicationService, AddressBlockApplicationService>();
        }
    }
}
