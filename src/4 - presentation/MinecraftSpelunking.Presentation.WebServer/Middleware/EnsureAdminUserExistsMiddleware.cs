using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Endpoints;
using MinecraftSpelunking.Application.AspNetCore.Common.Dtos;
using MinecraftSpelunking.Application.AspNetCore.Common.Services;
using MinecraftSpelunking.Domain.Identity.Common.Services;
using MinecraftSpelunking.Presentation.Common;
using MinecraftSpelunking.Presentation.WebServer.Attributes;

namespace MinecraftSpelunking.Presentation.WebServer.Middleware
{
    internal sealed class EnsureAdminUserExistsMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.GetEndpointMetadataCollection(out EndpointMetadataCollection? endpointMetadataCollection) == false)
            {
                await next(context);
                return;
            }

            // We only need to bother running this middleware on actual page requests
            // without this the blazor/_framework.js or whatever fails to load because of
            // the redirect. 
            // There are probably better ways to check this but this is good enough for now.
            Type componentType = endpointMetadataCollection.GetMetadata<ComponentTypeMetadata>()?.Type ?? typeof(object);
            if (componentType.IsAssignableTo<ComponentBase>() == false)
            {
                await next(context);
                return;
            }

            // Some pages dont need to check, this will skip the user check if the correct
            // attribute is attached to the request.
            bool ensureAdminUserExists = endpointMetadataCollection.GetMetaDataOrDefault(EnsureAdminUserExistsAttribute.Default).Value;
            if (ensureAdminUserExists == false)
            {
                await next(context);
                return;
            }

            IUserService users = context.RequestServices.GetRequiredService<IUserService>();
            if (users.Any() == false)
            {
                context.RequestServices.GetRequiredService<ILogger<EnsureAdminUserExistsMiddleware>>()
                    .LogInformation("No user accounts detected, redirecting to {CreateAdmin}", Constants.Routes.Account.RegisterAdmin);

                IRedirectApplicationService redirect = context.RequestServices.GetRequiredService<IRedirectApplicationService>();
                redirect.RedirectTo(uri: Constants.Routes.Account.RegisterAdmin, queryParameters: new Dictionary<string, object?>()
                {
                    { Constants.QueryParameters.ReturnUrl, context.Request.Path }
                }, statusMessage: StatusMessageDto.Warning("No Admin account detected."));
            }

            await next(context);
        }
    }
}
