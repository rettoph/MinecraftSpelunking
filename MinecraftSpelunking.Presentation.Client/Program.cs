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
    configuration.BaseAddress = "https://localhost:7244";
    configuration.AccessToken = "1c5eaada-8b19-4f6e-8f4f-0870ab6f5d12-40e41f41-fe6d-4d0d-91f2-34e7236bd4be";
});

builder.Services.AddScoped<IMinecraftSpelunkingClientService, MinecraftSpelunkingClientService>()
    .AddScoped<IAddressBlockClientService, AddressBlockClientService>()
    .AddScoped<PingJob>()
    .AddAutoMapper(typeof(ClientMapperProfile));

builder.Services.AddHostedService<JobRunner>();

await builder.Build().RunAsync();