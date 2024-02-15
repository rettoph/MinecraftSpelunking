using MinecraftSpelunking.Presentation.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterPresentationCommonServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Response).Assembly);

            return services;
        }
    }
}
