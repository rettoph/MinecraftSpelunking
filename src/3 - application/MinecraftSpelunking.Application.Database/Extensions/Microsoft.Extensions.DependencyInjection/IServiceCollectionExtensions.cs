using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.Extensions.Configuration;
using MinecraftSpelunking.Application.Database.Common.Services;
using MinecraftSpelunking.Application.Database.Services;
using MinecraftSpelunking.Domain.Database;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationDatabaseServices(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.RegisterDomainDatabaseServices(configuration);

            return services.AddScoped<IMapperApplicationService, MapperApplicationService>()
                .AddAutoMapper((provider, mapper) =>
                {
                    mapper.AddCollectionMappers();
                    mapper.UseEntityFrameworkCoreModel<DataContext>(provider);
                }, typeof(DataContext).Assembly);
        }
    }
}
