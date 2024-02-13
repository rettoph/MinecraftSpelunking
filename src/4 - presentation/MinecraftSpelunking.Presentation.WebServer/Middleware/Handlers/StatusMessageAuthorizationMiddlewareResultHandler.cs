using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using MinecraftSpelunking.Application.AspNetCore.Common.Dtos;
using MinecraftSpelunking.Application.AspNetCore.Common.Services;

namespace MinecraftSpelunking.Presentation.WebServer.Middleware.Handlers
{
    internal sealed class StatusMessageAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Succeeded == true)
            {
                await next.Invoke(context);
                return;
            }

            if (authorizeResult.Forbidden == true)
            {
                this.RedirectTo(context, Constants.Routes.Home, "You can not access this content.", StatusCodes.Status403Forbidden);
                return;
            }

            this.RedirectTo(context, Constants.Routes.Home, "You must be logged in to access the requested content.", StatusCodes.Status401Unauthorized);
        }

        private void RedirectTo(HttpContext context, string uri, string message, int statusCode)
        {
            context.Response.StatusCode = statusCode;

            IRedirectApplicationService redirects = context.RequestServices.GetRequiredService<IRedirectApplicationService>();
            redirects.RedirectTo(uri, new Dictionary<string, object?>()
            {
                { Constants.QueryParameters.ReturnUrl, context.Request.Path }
            }, StatusMessageDto.Warning(message));
        }
    }
}
