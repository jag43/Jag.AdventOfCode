using System;
using System.Linq;
using Jag.AdventOfCode;

namespace Jag.AdventOfCode.Y2021.Day7
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 7;

        public string SolvePart1(string input)
        {
            int[] crabs = input.Split(",").Select(i => int.Parse(i)).ToArray();
            int max = crabs.Max();

            var answers = new int[max];

            for (int i = 0; i < max; i++)
            {
                answers[i] = CalculateFuelPart1(i, crabs);
            }

            return answers.Min().ToString();
        }

        private int CalculateFuelPart1(int i, int[] crabs)
        {
            return crabs.Sum(crab =>
                crab == i ? 0
                : crab > i ? crab - i
                : i - crab);
        }
        private decimal CalculateFuelPart2(int i, int[] crabs)
        {
            return crabs.Sum(crab =>
            {
                decimal distance = crab == i ? 0
                : crab > i ? crab - i
                : i - crab;
                return (distance * (distance + 1)) / 2;
            });
        }

        public string SolvePart2(string input)
        {
            int[] crabs = input.Split(",").Select(i => int.Parse(i)).ToArray();
            int max = crabs.Max();

            var answers = new decimal[max];

            for (int i = 0; i < max; i++)
            {
                answers[i] = CalculateFuelPart2(i, crabs);
            }

            return answers.Min().ToString();
        }
    }
}