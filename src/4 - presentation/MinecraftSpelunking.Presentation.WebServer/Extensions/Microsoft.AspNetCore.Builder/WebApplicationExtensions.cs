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

				var currentAttempt = 0;
				var maxAttempts = 3;
				while (currentAttempt < maxAttempts)
				{
					try
					{
						if ((await context.Database.GetPendingMigrationsAsync()).Any())
						{
							context.Database.Migrate();
						}

						break;
					}
					catch (Exception ex)
					{
						var logger = services.GetRequiredService<ILogger<WebApplication>>();
						logger.LogError(ex, "There was an error applying migrations. Attempt {CurrentAttempt}/{MaxAttempts}", currentAttempt++, maxAttempts);

						if (currentAttempt == maxAttempts)
						{
							logger.LogError("Too many migration attempts failed. Goodbye.");
							return;
						}
					}
				}
			}

			await app.RunAsync();
		}
	}
}
