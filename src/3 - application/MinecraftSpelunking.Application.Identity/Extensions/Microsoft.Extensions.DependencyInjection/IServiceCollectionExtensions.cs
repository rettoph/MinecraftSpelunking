using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using MinecraftSpelunking.Application.Identity;
using MinecraftSpelunking.Application.Identity.Common;
using MinecraftSpelunking.Application.Identity.Common.AutoMapper;
using MinecraftSpelunking.Application.Identity.Common.Services;
using MinecraftSpelunking.Application.Identity.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationIdentityServices(this IServiceCollection services)
        {
            services.AddAuthorizationCore();

            services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(Constants.AuthenticationSchemas.Basic, null);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "MinecraftSpelunking",
                    Version = "v1"
                });

                options.AddSecurityDefinition(Constants.AuthenticationSchemas.Basic, new OpenApiSecurityScheme()
                {
                    Name = Constants.Headers.Authorization,
                    Type = SecuritySchemeType.Http,
                    Scheme = Constants.AuthenticationSchemas.Basic,
                    In = ParameterLocation.Header,
                    Description = "Basic authorization header"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = Constants.AuthenticationSchemas.Basic
                            }
                        },
                        new[] { Constants.AuthenticationSchemas.Basic }
                    }
                });
            });

            return services.RegisterDomainIdentityServices()
                .AddAutoMapper(typeof(ApplicationIdentityMapperProfile))
                .AddScoped<IUserApplicationService, UserApplicationService>();
        }
    }
}
