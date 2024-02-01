namespace MinecraftSpelunking.Presentation.WebServer.Extensions.Microsoft.DependencyInjection
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
