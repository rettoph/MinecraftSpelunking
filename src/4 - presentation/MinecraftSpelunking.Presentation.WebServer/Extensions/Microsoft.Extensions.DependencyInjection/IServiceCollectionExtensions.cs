namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection TryRegisterLettuceEncrypt(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration["LettuceEncrypt:Enabled"] == "true")
            {
                services.AddLettuceEncrypt();
            }

            return services;
        }
    }
}
