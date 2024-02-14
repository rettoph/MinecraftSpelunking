using MinecraftSpelunking.Domain.Minecraft.Common.Json;
using MinecraftSpelunking.Domain.Minecraft.Common.Services;
using MinecraftSpelunking.Domain.Minecraft.Services;
using JsonOptionsHttp = Microsoft.AspNetCore.Http.Json.JsonOptions;
using JsonOptionsMvc = Microsoft.AspNetCore.Mvc.JsonOptions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDomainMinecraftServices(this IServiceCollection services)
        {
            return services.AddScoped<IReservedAddressBlockService, ReservedAddressBlockService>()
                .AddScoped<IAddressBlockService, AddressBlockService>()
                .Configure<JsonOptionsMvc>(json =>
                {
                    json.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                    json.JsonSerializerOptions.Converters.Add(new IPNetwork2Converter());
                })
                .Configure<JsonOptionsHttp>(json =>
                {
                    json.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                    json.SerializerOptions.Converters.Add(new IPNetwork2Converter());
                });
        }
    }
}
