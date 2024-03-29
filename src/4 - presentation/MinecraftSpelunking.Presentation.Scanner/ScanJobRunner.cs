﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MinecraftSpelunking.Presentation.ClientApp.Jobs;
using MinecraftSpelunking.Presentation.Common.Models;
using MinecraftSpelunking.Presentation.Scanner;
using System.Diagnostics;

namespace MinecraftSpelunking.Presentation.ClientApp
{
    public sealed class ScanJobRunner : IHostedService
    {
        private IMinecraftSpelunkingApiClient _client;
        private IServiceProvider _provider;
        private ILogger<ScanJobRunner> _logger;
        private IOptions<MinecraftSpelunkingClientConfiguration> _configuration;
        private readonly DateTime _startTime;
        private TimeSpan _runTime;

        public ScanJobRunner(IMinecraftSpelunkingApiClient client, IServiceProvider provider, ILogger<ScanJobRunner> logger, IOptions<MinecraftSpelunkingClientConfiguration> configuration)
        {
            _client = client;
            _provider = provider;
            _logger = logger;
            _configuration = configuration;
            _startTime = DateTime.Now;
            _runTime = TimeSpan.FromHours(_configuration.Value.RunTimeInHours);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            int threadCount = _configuration.Value.ConcurrentTasks;

            AddressBlockAssignmentsModel? response = await _client.TryGetAsync(threadCount);
            if (response is null)
            {
                return;
            }

            ThreadPool.SetMaxThreads(threadCount, threadCount);

            int completedAssignments = 0;
            int foundJavaServers = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<Task<AddressBlockAssignmentResultsModel>> jobs = new List<Task<AddressBlockAssignmentResultsModel>>();
            for (int i = 0; i < response.Assignments.Length; i++)
            {
                jobs.Add(this.Run(response.Assignments[i]));
            }

            while (jobs.Count > 0)
            {
                Task<AddressBlockAssignmentResultsModel>? completed = await Task.WhenAny(jobs); ;
                jobs.Remove(completed);

                AddressBlockAssignmentModel? next = await _client.TryCompleteAsync(completed.Result);
                if (next is not null && (DateTime.Now - _startTime) < _runTime)
                {
                    jobs.Add(this.Run(next));
                }

                completedAssignments++;
                foundJavaServers += completed.Result.JavaServers.Length;

                int totalIps = completedAssignments * 1024;
                _logger.LogInformation("Completed {CompletedAssignments} assignments ({TotalIps} ips). Discovered {JavaServers} Java servers. Time elapsed: {TimeElapsed}. {IpsPerMinute} ips scanned per minute.",
                    completedAssignments, completedAssignments * 1024, foundJavaServers, stopwatch.Elapsed, totalIps / stopwatch.Elapsed.TotalMinutes);
            }

            _logger.LogInformation("No jobs running. Scans complete. Goodbye.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<AddressBlockAssignmentResultsModel> Run(AddressBlockAssignmentModel assignment)
        {
            using (IServiceScope scope = _provider.CreateScope())
            {
                ScanJob job = scope.ServiceProvider.GetRequiredService<ScanJob>();
                return job.ScanAsync(assignment);
            }
        }
    }
}
