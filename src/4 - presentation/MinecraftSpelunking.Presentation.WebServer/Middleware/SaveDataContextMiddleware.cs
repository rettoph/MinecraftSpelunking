
using MinecraftSpelunking.Domain.Database;

namespace MinecraftSpelunking.Presentation.WebServer.Middleware
{
    public class SaveDataContextMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            await next(httpContext);

            DataContext dataContext = httpContext.RequestServices.GetRequiredService<DataContext>();
            if (dataContext.ChangeTracker.Entries().Any())
            {
                await dataContext.SaveChangesAsync();
            }
        }
    }
}
