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

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            IRedirectApplicationService redirects = context.RequestServices.GetRequiredService<IRedirectApplicationService>();
            redirects.RedirectTo(Constants.Routes.Account.Login, new Dictionary<string, object?>()
            {
                { Constants.QueryParameters.ReturnUrl, context.Request.Path }
            }, StatusMessageDto.Error("You must be logged in to access the requested content."));
        }
    }
}
