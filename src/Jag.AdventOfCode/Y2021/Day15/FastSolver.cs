using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Jag.AdventOfCode.Utilities;
using Priority_Queue;

namespace Jag.AdventOfCode.Y2021.Day15
{
    public class FastSolver : ISolver
    {
        public int Year => 2021;

        public int Day => 15;

        public string SolvePart1(string input)
        {
            return FindShortestPath(ParseInput1(input)).ToString();
        }

        public string SolvePart2(string input)
        {
            return FindShortestPath(ParseInput2(input)).ToString();
        }

        private int FindShortestPath(int[][] grid)
        {
            var distances = Enumerable.Repeat(0, grid.Length)
                .Select(_ => Enumerable.Repeat(int.MaxValue, grid[0].Length).ToArray())
                .ToArray();

            var visited = new HashSet<(int row, int col)>();
            // for (int row = 0; row < grid.Length; row++)
            //     for (int col = 0; col < grid[0].Length; col++)
            //         unvisited.Add((row, col));

            distances[0][0] = 0;

            int lastRow = grid.Length - 1;
            int lastCol = grid[0].Length - 1;

            var priorityQueue = new SimplePriorityQueue<(int row, int col), int>();
            priorityQueue.Enqueue((0, 0), 0);
            
            while (true)
            {
                var position = priorityQueue.Dequeue();
                var (row, col) = position;

                if (visited.Contains(position))
                    continue;

                visited.Add(position);

                var currentScore = distances[row][col];

                if (position == (lastRow, lastCol))
                {
                    return currentScore;
                }

                var neighbours = grid.GetAdjacentNeighbours(position)
                    .Where(x => !visited.Contains((x.row, x.col)));
                foreach (var (nrow, ncol) in neighbours)
                {
                    var neighbourScore = currentScore + grid[nrow][ncol];
                    bool requeue = false;
                    if (distances[nrow][ncol] > neighbourScore)
                    {
                        distances[nrow][ncol] = neighbourScore;
                        requeue = true;
                    }
                    // just don't re-queue something if you didn't improve the metric
                    if (requeue)
                        priorityQueue.Enqueue((nrow, ncol), neighbourScore);
                }
            }
        }

        private int[][] ParseInput1(string input)
        {
            return input.Split(Environment.NewLine, options: SSO.Value)
                .Select(s => s.ToCharArray()
                    .Select(c => int.Parse(c.ToString()))
                    .ToArray())
                .ToArray();
        }
        private int[][] ParseInput2(string input)
        {
            var grid1 = ParseInput1(input);
            var grid2 = Enumerable.Repeat(0, grid1.Length * 5)
                .Select(_ => Enumerable.Repeat(0, grid1[0].Length * 5).ToArray())
                .ToArray();
            // var lastRow = grid1.Length - 1;
            // var lastCol = 
            for (int row = 0; row < grid2.Length; row++)
            {
                for (int col = 0; col < grid2[0].Length; col++)
                {
                    var offset = (row / grid1.Length) + (col / grid1[0].Length);
                    var baseRisk = grid1[row % grid1.Length][col % grid1.Length];
                    grid2[row][col] = offset == 0 ? baseRisk : ((baseRisk - 1 + offset) % 9) + 1;
                }
            }
            return grid2;
        }
    }
}