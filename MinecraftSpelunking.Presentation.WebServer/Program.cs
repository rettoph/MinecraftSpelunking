using AutoMapper;
using AutoMapper.EquivalencyExpression;
using MinecraftSpelunking.Application.Extensions.Microsoft.DependencyInjection;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Extensions.Microsoft.DependencyInjection;
using MinecraftSpelunking.Presentation.WebServer.AutoMapper;
using MinecraftSpelunking.Presentation.WebServer.Middleware;

var builder = WebApplication.CreateBuilder();

builder.WebHost.UseKestrel().UseUrls(builder.Configuration["Url"] ?? throw new Exception());

builder.Services
    .RegisterDomainServices(builder.Configuration)
    .RegisterApplicationServices()
    .AddAutoMapper(typeof(WebServerMapperProfile))
    .AddAutoMapper((provider, mapper) =>
    {
        mapper.AddCollectionMappers();
        mapper.UseEntityFrameworkCoreModel<DataContext>(provider);
    }, typeof(DataContext).Assembly);


// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<AccessTokenMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
