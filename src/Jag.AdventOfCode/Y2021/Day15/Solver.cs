using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;

namespace Jag.AdventOfCode.Y2021.Day15
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 15;
        private static readonly (int col, int row)[]  RelativeNeighbours = new (int col, int row)[]
            {
                (-1, 0), // down
                (0, -1), // left
                (0, 1), // up
                (1, 0), // down
            };
        public string SolvePart1(string input)
        {
            return FindShortestPath(ParseInput(input)).ToString();
        }

        public string SolvePart2(string input)
        {
            throw new NotImplementedException();
        }

        private int FindShortestPath(int[][] grid)
        {
            var priorityQueue = new SimplePriorityQueue<((int row, int col) current, HashSet<(int row, int col)> visited), int>();
            
            priorityQueue.Enqueue(((0, 0), new HashSet<(int, int)> { (0, 0) }), 0);

            int lastRow = grid.Length - 1;
            int lastCol = grid[0].Length - 1;

            while (priorityQueue.Count != 0)
            {
                var ((row, col), currentPath) = priorityQueue.Dequeue();
                var neighbours = RelativeNeighbours.Select(n => (row: n.row + row, col: n.col + col))
                    .Where(x => x.col >= 0 && x.row >= 0 && x.col <= lastCol && x.row <= lastRow && !currentPath.Contains((x.row, x.col)))
                    .OrderBy(x => grid[x.row][x.col]);
                foreach (var (nrow, ncol) in neighbours)
                {
                    var newCurrentPath = new HashSet<(int row, int col)>(currentPath);
                    newCurrentPath.Add((nrow, ncol));
                    var score = newCurrentPath.Sum(x => grid[x.row][x.col]);
                    if ((ncol, nrow) == (lastCol, lastRow))
                    {
                        return score - grid[0][0];
                    }
                    priorityQueue.Enqueue(((nrow, ncol), new HashSet<(int row, int col)>(newCurrentPath)), score);
                }
            }
            
            throw new Exception();
        }

        private int[][] ParseInput(string input)
        {
            return input.Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.ToCharArray()
                    .Select(c => int.Parse(c.ToString()))
                    .ToArray())
                .ToArray();
        }
    }
}