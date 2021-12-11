using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day11
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 11;

        public string SolvePart1(string input)
        {
            var dumbo = ParseInput(input);
            int steps = 100;
            int flashes = 0;
            for (int step = 1; step <= steps; step++)
            {
                for (int i = 0; i < dumbo.Length; i++)
                {
                    var row = dumbo[i];
                    for (int k = 0; k < row.Length; k++)
                    {
                        flashes += Increment(dumbo, i, k);
                    }
                }
                for (int i = 0; i < dumbo.Length; i++)
                {
                    var row = dumbo[i];
                    for (int k = 0; k < row.Length; k++)
                    {
                        if (dumbo[i][k] > 9)
                        {
                            dumbo[i][k] = 0;
                        }
                    }
                }
                Console.WriteLine($"After Step:{step}, cum flashes: {flashes}, 0s this step: {dumbo.SelectMany(i => i).Where(i => i == 0).Count()}");
                // WriteToConsole(dumbo);
                // Console.WriteLine();
            }

            return flashes.ToString();
        }

        private void WriteToConsole(int[][] dumbo)
        {
            for (int i = 0; i < dumbo.Length; i++)
            {
                var row = dumbo[i];
                for (int k = 0; k < row.Length; k++)
                {
                    Console.Write(dumbo[i][k] > 9 ? "0" : dumbo[i][k].ToString());
                }
                Console.WriteLine();
            }
        }

        private int Increment(int[][] dumbo, int i, int k)
        {
            dumbo[i][k]++;
            int flashes = 0;
            if (dumbo[i][k] == 10)
            {
                flashes++;
                var neighbours = GetNeighbours(dumbo, i, k);
                foreach (var neighbour in neighbours)
                {
                    flashes += Increment(dumbo, neighbour.i, neighbour.k);
                }
            }
            return flashes;
        }

        private IEnumerable<(int i, int k)> GetNeighbours(int[][] dumbo, int i, int k)
        {
            int lastRow = dumbo.Length - 1;
            int lastCol = dumbo[0].Length - 1;

            var neighbours = new (int i, int k)[] 
            {
                (-1, -1),  (-1, 0), (-1, 1), 
                (0, -1), (0, 1),
                (1, -1), (1, 0), (1, 1)
            };
            foreach (var neighbour in neighbours.Select(n => (i: n.i + i, k: n.k + k)))
            {
                if (neighbour.i >= 0 && neighbour.i <= lastRow
                    && neighbour.k >= 0 && neighbour.k <= lastCol)
                {
                    yield return (neighbour);
                }
            }
        }

        public string SolvePart2(string input)
        {
            var dumbo = ParseInput(input);
            var dumboCount = dumbo.Length * dumbo[0].Length;
            int flashes = 0;
            for (int step = 1; step <= int.MaxValue; step++)
            {
                for (int i = 0; i < dumbo.Length; i++)
                {
                    var row = dumbo[i];
                    for (int k = 0; k < row.Length; k++)
                    {
                        flashes += Increment(dumbo, i, k);
                    }
                }
                for (int i = 0; i < dumbo.Length; i++)
                {
                    var row = dumbo[i];
                    for (int k = 0; k < row.Length; k++)
                    {
                        if (dumbo[i][k] > 9)
                        {
                            dumbo[i][k] = 0;
                        }
                    }
                }

                var flashesThisStep = dumbo.SelectMany(i => i).Where(i => i == 0).Count();
                Console.WriteLine($"After Step:{step}, cum flashes: {flashes}, 0s this step: {flashesThisStep}");
                // WriteToConsole(dumbo);
                // Console.WriteLine();
                if (dumboCount == flashesThisStep)
                {
                    return step.ToString();
                }
            }
            throw new Exception();
        }

        public int[][] ParseInput(string input)
        {
            return input.Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s
                    .Select(c => int.Parse(c.ToString()))
                    .ToArray())
                .ToArray();
        }
    }
}