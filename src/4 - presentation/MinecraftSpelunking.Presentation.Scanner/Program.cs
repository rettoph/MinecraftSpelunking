using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinecraftSpelunking.Presentation.ClientApp;
using MinecraftSpelunking.Presentation.ClientApp.Jobs;

var builder = new HostApplicationBuilder();
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.Configure<MinecraftSpelunkingClientConfiguration>(client =>
{
    builder.Configuration.GetSection("MinecraftSpelunkingClientConfiguration").Bind(client);
});

builder.Services.RegisterScannerServices();

builder.Services.AddScoped<ScanJob>();

builder.Services.AddHostedService<ScanJobRunner>();

await builder.Build().RunAsync();