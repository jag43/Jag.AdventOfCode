using System;
using System.Collections.Generic;
using System.Linq;
using Jag.AdventOfCode.Utilities;

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
            }

            return flashes.ToString();
        }

        private int Increment(int[][] dumbo, int i, int k)
        {
            dumbo[i][k]++;
            int flashes = 0;
            if (dumbo[i][k] == 10)
            {
                flashes++;
                var neighbours = dumbo.GetNeighbours((i, k));
                foreach (var neighbour in neighbours)
                {
                    flashes += Increment(dumbo, neighbour.row, neighbour.col);
                }
            }
            return flashes;
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
