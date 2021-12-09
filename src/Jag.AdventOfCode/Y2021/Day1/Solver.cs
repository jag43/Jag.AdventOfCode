using System;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day1
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 1;

        public string SolvePart1(string input)
        {
            int [] readings = ParseInput(input);

            int diffs = 0;
            int prev = readings[0];

            for (int i = 1; i < readings.Length; i++)
            {
                if (readings[i] > prev)
                {
                    diffs++;
                }
                prev = readings[i];
            }

            return diffs.ToString();
        }

        public string SolvePart2(string input)
        {
            int [] readings = ParseInput(input);

            int diffs = 0;
            int prev = readings[0];

            for (int i = 2; i < readings.Length - 1; i++)
            {
                if (readings[i + 1] > prev)
                {
                    diffs++;
                }
                prev = readings[i - 1];
            }

            return diffs.ToString();
        }

        private int[] ParseInput(string input)
        {
            return input.Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .ToArray();
        }
    }
}