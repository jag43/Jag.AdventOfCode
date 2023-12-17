using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2023.Day9
{
    public class Solver : ISolver
    {
        public int Year => 2023;

        public int Day => 9;

        public string SolvePart1(string input)
        {
            var histories = input.Split(Environment.NewLine, SSO.Value)
                .Select(line => line.Split(" ", SSO.Value)
                    .Select(long.Parse)
                    .ToList())
                .ToList();

            var nextValues = new List<long>();

            foreach (var history in histories)
            {
                long nextValue = CalculateNextValue(history);
                nextValues.Add(history.Last() + nextValue);
            }

            return nextValues.Sum().ToString();
        }

        private long CalculateNextValue(List<long> history)
        {
            List<List<long>> differenceList = [ CalculateDifferences(history) ];
            while (!differenceList.Last().All(d => d == 0))
            {
                differenceList.Add(CalculateDifferences(differenceList.Last()));
            }
            
            for (int i = differenceList.Count - 1; i > 0; i--)
            {
                var current = differenceList[i];
                var prev = differenceList[i - 1];
                prev.Add(prev.Last() + current.Last());
            }
            return differenceList[0].Last();
        }

        private static List<long> CalculateDifferences(List<long> history)
        {
            var differences = new List<long>();
            for (int i = 1; i < history.Count; i++)
            {
                differences.Add(history[i] - history[i - 1]);
            }
            return differences;
        }

        public string SolvePart2(string input)
        {
             var histories = input.Split(Environment.NewLine, SSO.Value)
                .Select(line => line.Split(" ", SSO.Value)
                    .Select(long.Parse)
                    .Reverse()
                    .ToList())
                .ToList();

            var nextValues = new List<long>();

            foreach (var history in histories)
            {
                long nextValue = CalculateNextValue(history);
                nextValues.Add(history.Last() + nextValue);
            }

            return nextValues.Sum().ToString();
        }
    }
}