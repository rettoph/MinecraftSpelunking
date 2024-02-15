using Microsoft.AspNetCore.Mvc;
using MinecraftSpelunking.Presentation.Common;

namespace MinecraftSpelunking.Presentation.WebServer.Controllers
{
    public abstract class MinecraftSpelunkingBaseController : ControllerBase
    {
        protected new Response<T> Response<T>(T? model, int statusCode = StatusCodes.Status200OK, string? message = null)
        {
            this.HttpContext.Response.StatusCode = statusCode;

            return new Response<T>()
            {
                StatusCode = statusCode,
                Message = message,
                Model = model
            };
        }
    }
}
