using MinecraftSpelunking.Domain.Database.Extensions.Microsoft.DependencyInjection;
using MinecraftSpelunking.Domain.Identity.Extensions.Microsoft.DependencyInjection;
using MinecraftSpelunking.Presentation.WebServer.Extensions.Microsoft.AspNetCore.Builder;
using MinecraftSpelunking.Presentation.WebServer.Extensions.Microsoft.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.TryRegisterLettuceEncrypt(builder.Configuration)
    .RegisterDatabaseServices(builder.Configuration)
    .RegisterIdentityServices();

#if DEBUG
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();
#elif RELEASE
builder.Services.AddControllersWithViews();
#endif

WebApplication app = builder.Build();

#if RELEASE
app.UseExceptionHandler("/Home/Error");
#endif

app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.ApplyMigrationsAndRunAsync();
