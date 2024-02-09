using MinecraftSpelunking.Domain.Database.Extensions.Microsoft.DependencyInjection;
using MinecraftSpelunking.Domain.Identity.Extensions.Microsoft.DependencyInjection;
using MinecraftSpelunking.Presentation.WebServer.Components;
using MinecraftSpelunking.Presentation.WebServer.Extensions.Microsoft.AspNetCore.Builder;
using MinecraftSpelunking.Presentation.WebServer.Extensions.Microsoft.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);



builder.Services
    .TryRegisterLettuceEncrypt(builder.Configuration)
    .RegisterDatabaseServices(builder.Configuration)
    .RegisterIdentityServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

WebApplication app = builder.Build();

#if RELEASE
app.UseExceptionHandler("/Home/Error");
#endif

app.UseHsts();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.ApplyMigrationsAndRunAsync();
