using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using MinecraftSpelunking.Application.AspNetCore.Common.Dtos;
using MinecraftSpelunking.Application.AspNetCore.Common.Services;
using MinecraftSpelunking.Presentation.Common;

namespace MinecraftSpelunking.Presentation.WebServer.Middleware.Handlers
{
    internal sealed class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Succeeded == true)
            {
                await next.Invoke(context);
                return;
            }

            if (context.Request.Path.Value?.StartsWith("/api/") ?? false)
            {
                await this.ApiHandleAsync(next, context, policy, authorizeResult);
                return;
            }

            await this.WebHandleAsync(next, context, policy, authorizeResult);
            return;
        }

        private async Task ApiHandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden == true)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new Response()
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "Forbidden"
                });
                return;
            }

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new Response()
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Message = "Unauthorized"
            });
            return;
        }

        private Task WebHandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden == true)
            {
                this.RedirectTo(context, Constants.Routes.Home, "You can not access this content.", StatusCodes.Status403Forbidden);
                return Task.CompletedTask;
            }

            this.RedirectTo(context, Constants.Routes.Home, "You must be logged in to access the requested content.", StatusCodes.Status401Unauthorized);
            return Task.CompletedTask;
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
