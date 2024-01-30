using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinecraftSpelunking.Presentation.Client;
using MinecraftSpelunking.Presentation.Client.AutoMapper;
using MinecraftSpelunking.Presentation.Client.Configurations;
using MinecraftSpelunking.Presentation.Client.Jobs;
using MinecraftSpelunking.Presentation.Client.Services;
using MinecraftSpelunking.Presentation.Client.Services.Implementations;

var builder = new HostApplicationBuilder();

builder.Services.Configure<MinecraftSpelunkingClientConfiguration>(configuration =>
{
    configuration.BaseAddress = "https://mcsp.rettoph.io";
    configuration.AccessToken = "dfb32051-3720-4b92-8c22-a356f842a508-9d204b86-ec6c-4f0a-b7a6-74758aaf6b14";
});

builder.Services.AddScoped<IMinecraftSpelunkingClientService, MinecraftSpelunkingClientService>()
    .AddScoped<IAddressBlockClientService, AddressBlockClientService>()
    .AddScoped<PingJob>()
    .AddAutoMapper(typeof(ClientMapperProfile));

builder.Services.AddHostedService<JobRunner>();

await builder.Build().RunAsync();