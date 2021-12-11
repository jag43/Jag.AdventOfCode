using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jag.AdventOfCode.Runner
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly ILogger logger;

        public Worker(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime, ILogger<Worker> logger)
        {
            this.serviceProvider = serviceProvider;
            this.hostApplicationLifetime = hostApplicationLifetime;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                try
                {
                    var puzzleService = scope.ServiceProvider.GetRequiredService<PuzzleService>();
                    await puzzleService.SolveProblemAsync();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Error occurred during {nameof(ExecuteAsync)}: {ex.Message}");
                }

                hostApplicationLifetime.StopApplication();
            }
        }
    }
}
