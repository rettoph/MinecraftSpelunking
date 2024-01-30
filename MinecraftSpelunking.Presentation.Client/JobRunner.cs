using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinecraftSpelunking.Presentation.Client.Jobs;

namespace MinecraftSpelunking.Presentation.Client
{
    public class JobRunner : IHostedService
    {
        private IServiceProvider _provider;

        public JobRunner(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            int threadCount = 256 / 2;

            ThreadPool.SetMaxThreads(threadCount, threadCount);

            for (int i = 0; i < threadCount; i++)
            {
                Task.Run(this.Run);
                Thread.Sleep(250);
            }


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Run()
        {
            using (IServiceScope scope = _provider.CreateScope())
            {
                PingJob job = scope.ServiceProvider.GetRequiredService<PingJob>();
                await job.StartAsync();
            }
        }
    }
}
