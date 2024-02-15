using MinecraftSpelunking.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCommonServices(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(Page<>).Assembly);
        }
    }
}
