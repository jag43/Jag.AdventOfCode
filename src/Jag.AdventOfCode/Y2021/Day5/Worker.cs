// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;

// namespace Day5
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
//             await Task.Delay(TimeSpan.FromSeconds(0.5));
//             var lines = MapLinesFromData(Data.Input);
//             var allPositions = lines.SelectMany(lr => new[] { lr.start, lr.end });
//             var maxX = allPositions.Max(pos => pos.x);
//             var maxY = allPositions.Max(pos => pos.y);

//             var grid = new int[maxX + 1, maxY + 1];
//             var gridWithDiagonals = new int[maxX + 1, maxY + 1];

//             foreach (var line in lines)
//             {
//                 var gridSquares = GetPositionsBetweenPositions(line);
//                 if (line.IsDiagonal())
//                 {
//                     foreach (var square in gridSquares)
//                     {
//                         gridWithDiagonals[square.x, square.y]++;
//                     }
//                 }
//                 else 
//                 {
//                     foreach (var square in gridSquares)
//                     {
//                         grid[square.x, square.y]++;
//                         gridWithDiagonals[square.x, square.y]++;
//                     }
//                 }
//             }

//             var task1 = grid.Cast<int>()
//                 .GroupBy(i => i)
//                 .Select(g => new { i = g.Key, count = g.Count() })
//                 .OrderBy(g => g.i)
//                 .ToList();
//             //Visualise(gridWithDiagonals);
//             _logger.LogInformation("No diagonals: " + Newtonsoft.Json.JsonConvert.SerializeObject(task1));
//             _logger.LogInformation("Num squares >= 2: " + task1.Where(g => g.i >= 2).Sum(g => g.count));
                
//             var task2 = gridWithDiagonals.Cast<int>()
//                 .GroupBy(i => i)
//                 .Select(g => new { i = g.Key, count = g.Count() })
//                 .OrderBy(g => g.i)
//                 .ToList();
//             _logger.LogInformation("Diagonals:" + Newtonsoft.Json.JsonConvert.SerializeObject(task2));
//             _logger.LogInformation("Num squares >= 2: " + task2.Where(g => g.i >= 2).Sum(g => g.count));

//             hostApplicationLifetime.StopApplication();
//         }

//         private void Visualise(int[,] grid)
//         {
//             for (int i = 0; i < grid.GetUpperBound(0) + 1; i++)
//             {
//                 for (int k = 0; k < grid.GetUpperBound(1) + 1; k++)
//                 {
//                     Console.Write(grid[k,i] == 0 ? "." : grid[k,i]);
//                 }
//                 Console.WriteLine();
//             }
//         }

//         private IEnumerable<Position> GetPositionsBetweenPositions((Position start, Position end) line)
//         {
//             var (start, end) = line;

//             if (line.start.x == line.end.x)
//             {
//                 var (starty, endy) = (line.start.y, line.end.y);
//                 for (int y = starty; y != endy; y = starty < endy ? y + 1 : y - 1)
//                 {
//                     yield return new Position(start.x, y);
//                 }
//             }
//             else if (line.start.y == line.end.y)
//             {
//                 var (startx, endx) = (line.start.x, line.end.x);
//                 for (int x = startx; x != endx; x = startx < endx ? x + 1 : x - 1)
//                 {
//                     yield return new Position(x, line.start.y);
//                 }
//             }
//             else
//             {
//                 //diagonal 
//                 var (startx, endx) = (line.start.x, line.end.x);
//                 var squarex = new List<int>();
//                 for (int x = startx; x != endx; x = startx < endx ? x + 1 : x - 1)
//                 {
//                     squarex.Add(x);
//                 }
//                 var (starty, endy) = (line.start.y, line.end.y);
//                 var squarey = new List<int>();
//                 for (int y = starty; y != endy; y = starty < endy ? y + 1 : y - 1)
//                 {
//                     squarey.Add(y);
//                 }

//                 for (int i = 0; i < squarex.Count; i++)
//                 {
//                     yield return new Position(squarex[i], squarey[i]);
//                 }
//             }

//             yield return end;
//         }

//         private List<(Position start, Position end)> MapLinesFromData(string value)
//         {
//             var lines = new List<(Position, Position)>();
//             var rows = value.Split(Environment.NewLine);
            
//             foreach (var row in rows)
//             {
//                 var positions = row.Split(" -> ");
//                 var leftStrings = positions[0].Split(",");
//                 var rightStrings = positions[1].Split(",");
//                 var left = new Position(int.Parse(leftStrings[0]), int.Parse(leftStrings[1]));
//                 var right = new Position(int.Parse(rightStrings[0]), int.Parse(rightStrings[1]));
//                 lines.Add((left, right));
//             }
//             return lines;
//         }
    
//     }

//     public record Position (int x, int y);

//     public static class PositionExtensions
//     {
//         public static bool IsDiagonal(this (Position start, Position end) line)
//         {
//             return line.start.x != line.end.x && line.start.y != line.end.y;
//         }
//     }
// }
