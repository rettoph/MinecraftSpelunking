using AutoMapper;
using AutoMapper.EquivalencyExpression;
using MinecraftSpelunking.Application.AspNetCore.Extensions.Microsoft.DependencyInjection;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Database.Extensions.Microsoft.DependencyInjection;
using MinecraftSpelunking.Presentation.WebServer.Components;
using MinecraftSpelunking.Presentation.WebServer.Extensions.Microsoft.AspNetCore.Builder;
using MinecraftSpelunking.Presentation.WebServer.Extensions.Microsoft.DependencyInjection;
using MinecraftSpelunking.Presentation.WebServer.Middleware;

var builder = WebApplication.CreateBuilder(args);



builder.Services
    .TryRegisterLettuceEncrypt(builder.Configuration)
    .RegisterDatabaseServices(builder.Configuration)
    .RegisterApplicationIdentityServices()
    .RegisterAspNetCoreServices()
    .AddAutoMapper((provider, mapper) =>
    {
        mapper.AddCollectionMappers();
        mapper.UseEntityFrameworkCoreModel<DataContext>(provider);
    }, typeof(DataContext).Assembly);

builder.Services
    .AddOptions()
    .AddAuthenticationCore()
    .AddAuthenticationCore()
    .AddCascadingAuthenticationState()
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<EnsureAdminUserExistsMiddleware>();

WebApplication app = builder.Build();

#if RELEASE
app.UseExceptionHandler("/Home/Error");
#endif


app.UseHsts();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseMiddleware<EnsureAdminUserExistsMiddleware>();
app.UseAuthorization();
app.UseAuthentication();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.ApplyMigrationsAndRunAsync();
