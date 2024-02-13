using MinecraftSpelunking.Domain.Minecraft.Common.Services;
using MinecraftSpelunking.Domain.Minecraft.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainMinecraftServices(this IServiceCollection services)
        {
            return services.AddScoped<IReservedAddressBlockService, ReservedAddressBlockService>()
                .AddScoped<IAddressBlockService, AddressBlockService>();
        }
    }
}
