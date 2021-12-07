// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using Newtonsoft.Json;

// namespace Day4
// {
//     public class Worker : BackgroundService
//     {
//         private readonly IHostApplicationLifetime hostApplicationLifetime;
//         private readonly ILogger<Worker> logger;

//         public Worker(IHostApplicationLifetime hostApplicationLifetime, ILogger<Worker> logger)
//         {
//             this.hostApplicationLifetime = hostApplicationLifetime;
//             this.logger = logger;
//         }

//         protected override Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             var sw = Stopwatch.StartNew();
//             var bingoData = BingoData.CreateFromString(Data.Value);

//             BingoBoard winner = null;

//             foreach(var board in bingoData.Boards)
//             {
//                 if (winner == null || winner.AchievesBingo() < board.AchievesBingo())
//                 {
//                     winner = board;
//                 }
//             }
            
//             var achievesBingo = winner.AchievesBingo();
//             var lastBingoNumber = bingoData.Numbers[achievesBingo];
//             var allSquares = winner.Rows.SelectMany(row => row);
//             var allUnmarked = allSquares.Where(square => square.Selected > achievesBingo);
//             var sum = allUnmarked.Sum(sq => sq.Number);

//             var answer = sum * lastBingoNumber;
//             sw.Stop();
//             logger.LogInformation(JsonConvert.SerializeObject(new {answer, sum, lastBingoNumber, achievesBingo, sw.ElapsedMilliseconds}));

//             hostApplicationLifetime.StopApplication();
//             return Task.CompletedTask;
//         }
//     }
// }
