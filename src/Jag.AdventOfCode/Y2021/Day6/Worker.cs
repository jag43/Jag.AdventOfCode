// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using Newtonsoft.Json;

// namespace Day6
// {
//     public class Worker : BackgroundService
//     {
//         private readonly IHostApplicationLifetime hostApplicationLifetime;
//         private readonly ILogger<Worker> _logger;

//         public Worker(IHostApplicationLifetime hostApplicationLifetime, ILogger<Worker> logger)
//         {
//             this.hostApplicationLifetime = hostApplicationLifetime;
//             _logger = logger;
//         }

//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             try{
//             await Task.Delay(500, stoppingToken);
//             var sw = Stopwatch.StartNew();
//             var fish = Data.Input
//                 .Split(",")
//                 .Select(s => int.Parse(s))
//                 .ToList();

//             var buckets = Enumerable.Repeat(0L, 9).ToArray();
//             foreach (var f in fish)
//             {
//                 buckets[f]++;
//             }

//             for (int i = 0; i < 256; i++)
//             {
//                 var prevBuckets = buckets;
//                 buckets = Enumerable.Repeat(0L, 9).ToArray();
//                 for (int k = 0; k < buckets.Length; k++)
//                 {
//                     if (k == 0)
//                     {
//                         buckets[8] = prevBuckets[0];
//                         buckets[6] = prevBuckets[0];
//                     }
//                     else 
//                     {
//                         buckets[k-1] += prevBuckets[k];
//                     }
//                 }

//                 // _logger.LogInformation($"{i}: {buckets.Sum():N0}{Environment.NewLine}time: {sw.Elapsed.TotalSeconds:N}{Environment.NewLine}{JsonConvert.SerializeObject(buckets)}");
//                 stoppingToken.ThrowIfCancellationRequested();
//             }

//             sw.Stop();

//             _logger.LogInformation("count: " + buckets.Sum());
//             _logger.LogInformation("EM: " + sw.ElapsedMilliseconds);

//             }
//             catch(Exception ex)
//             {
//                 _logger.LogError(ex, ex.Message);
//             }
//             hostApplicationLifetime.StopApplication();
//         }
//     }
// }
