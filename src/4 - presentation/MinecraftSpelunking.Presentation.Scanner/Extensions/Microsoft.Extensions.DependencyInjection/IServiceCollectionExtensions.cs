using Microsoft.Extensions.Options;
using MinecraftSpelunking.Presentation.ClientApp;
using MinecraftSpelunking.Presentation.Scanner;
using MinecraftSpelunking.Presentation.Scanner.Implementations;
using Polly;
using Polly.Extensions.Http;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterScannerServices(this IServiceCollection services)
        {
            services.AddHttpClient<IMinecraftSpelunkingApiClient, MinecraftSpelunkingApiClient>((p, http) =>
            {
                IOptions<MinecraftSpelunkingClientConfiguration> configuration = p.GetRequiredService<IOptions<MinecraftSpelunkingClientConfiguration>>();

                http.BaseAddress = new Uri(configuration.Value.BaseAddress);

                string authenticationString = $"{configuration.Value.ClientId}:{configuration.Value.ClientSecret}";
                string base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));
                http.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy());

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
