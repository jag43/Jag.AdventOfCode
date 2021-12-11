using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Jag.AdventOfCode.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jag.AdventOfCode.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    AddOptions(services, hostContext);

                    services.AddScoped<PuzzleService>();
                    services.AddScoped<InputRepository>();
                    services.AddScoped<AnswerRepository>();

                    services.AddHttpClient<AocHttpClient>();
                });

        private static void AddOptions(IServiceCollection services, HostBuilderContext hostContext)
        {
            var sourceRootConfig = hostContext.Configuration.GetSection("AdventOfCode:SourceRootDirectory").Get<SourceRootConfig>();
            services.AddSingleton(sourceRootConfig);

            var aocHttpConfig = hostContext.Configuration.GetSection("AdventOfCode:Http").Get<AocHttpConfig>();
            services.AddSingleton(aocHttpConfig);
        }
    }
}
