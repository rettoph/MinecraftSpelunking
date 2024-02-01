using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Domain.Database;

namespace MinecraftSpelunking.Presentation.WebServer.Extensions.Microsoft.AspNetCore.Builder
{
    public static class WebApplicationExtensions
    {
        public static async Task ApplyMigrationsAndRunAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<DataContext>();
                if ((await context.Database.GetPendingMigrationsAsync()).Any())
                {
                    context.Database.Migrate();
                }
            }

            await app.RunAsync();
        }
    }
}
