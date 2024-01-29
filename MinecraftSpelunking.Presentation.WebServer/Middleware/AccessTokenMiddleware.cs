using Microsoft.AspNetCore.Http.Features;
using MinecraftSpelunking.Application.Account.Services;
using MinecraftSpelunking.Common.Account.Entities;
using MinecraftSpelunking.Presentation.WebServer.Attributes;
using System.Net;

namespace MinecraftSpelunking.Presentation.WebServer.Middleware
{
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public AccessTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            IAccountApplicationService accounts = context.RequestServices.GetRequiredService<IAccountApplicationService>();
            IEndpointFeature endpointFeatures = context.Features.Get<IEndpointFeature>() ?? throw new Exception();

            IEnumerable<AccessTokenAttribute> accessTokenAttributes = endpointFeatures.Endpoint?.Metadata.OfType<AccessTokenAttribute>() ?? Array.Empty<AccessTokenAttribute>();

            if (accessTokenAttributes.Any() == false)
            {
                await _next(context);
                return;
            }

            string? accessToken = context.Request.Query["access_token"];
            if (accessToken is null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            User? user = await accounts.TrySignInWithApiAccessToken(
                apiAccessToken: accessToken,
                roles: accessTokenAttributes.SelectMany(x => x.RequiredRoles).Distinct().ToArray());

            if (user is null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}
