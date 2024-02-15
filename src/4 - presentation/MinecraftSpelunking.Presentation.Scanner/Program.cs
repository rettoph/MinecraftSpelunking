using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinecraftSpelunking.Presentation.ClientApp;
using MinecraftSpelunking.Presentation.ClientApp.Jobs;

var builder = new HostApplicationBuilder();

builder.Services.Configure<MinecraftSpelunkingClientConfiguration>(configuration =>
{
    configuration.BaseAddress = "https://localhost:8081";
    configuration.ClientUserName = "anthony@turner.ec";
    configuration.ClientSecret = "Password1!";
});

builder.Services.RegisterScannerServices();

builder.Services.AddScoped<PingJob>();

builder.Services.AddHostedService<JobRunner>();

await builder.Build().RunAsync();