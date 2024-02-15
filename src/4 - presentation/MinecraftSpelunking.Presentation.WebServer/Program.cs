using Microsoft.AspNetCore.Authorization;
using MinecraftSpelunking.Application.AspNetCore.Extensions.Microsoft.DependencyInjection;
using MinecraftSpelunking.Presentation.WebServer;
using MinecraftSpelunking.Presentation.WebServer.AutoMapper;
using MinecraftSpelunking.Presentation.WebServer.Components;
using MinecraftSpelunking.Presentation.WebServer.Extensions.Microsoft.AspNetCore.Builder;
using MinecraftSpelunking.Presentation.WebServer.Middleware;
using MinecraftSpelunking.Presentation.WebServer.Middleware.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .TryRegisterLettuceEncrypt(builder.Configuration)
    .RegisterPresentationCommonServices()
    .RegisterApplicationDatabaseServices(builder.Configuration)
    .RegisterApplicationIdentityServices()
    .RegisterAspNetCoreServices()
    .RegisterApplicationMinecraftServices()
    .ConfigureApplicationCookie(options =>
    {
        options.LoginPath = Constants.Routes.Account.Login;
    });

builder.Services
    .AddOptions()
    .AddCascadingAuthenticationState();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.AddTransient<EnsureAdminUserExistsMiddleware>()
    .AddTransient<SaveDataContextMiddleware>()
    .AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>()
    .AddAutoMapper(typeof(WebServerProfile).Assembly);

WebApplication app = builder.Build();

#if RELEASE
app.UseExceptionHandler("/Home/Error");
#endif

app.UseHsts();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseMiddleware<EnsureAdminUserExistsMiddleware>();
app.UseMiddleware<SaveDataContextMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "MinecraftSpelunking v1");
});

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

await app.ApplyMigrationsAndRunAsync();
