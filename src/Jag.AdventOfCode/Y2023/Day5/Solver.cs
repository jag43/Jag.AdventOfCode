using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2023.Day5
{
    public class Solver : ISolver
    {
        private const StringSplitOptions sso = SSO.Value;

        public int Year => 2023;

        public int Day => 5;

        public string SolvePart1(string input)
        {
            var inputSections = input.Split(Environment.NewLine + Environment.NewLine, sso);
            var seeds = inputSections[0].Split(' ', sso).Skip(1).Select(long.Parse).ToList();

            var mapSets = inputSections.Skip(1).Select(MapSet.Parse).ToList();

            var outputSeeds = new List<long>();

            foreach (var seed in seeds)
            {
                var currentLocation = seed;
                foreach (var mapSet in mapSets)
                {
                    currentLocation = mapSet.MapSourceToDestination(currentLocation);
                }
                outputSeeds.Add(currentLocation);
            }

            return outputSeeds.Min().ToString();
        }

        public string SolvePart2(string input)
        {
            var inputSections = input.Split(Environment.NewLine + Environment.NewLine, sso);
            var seedRanges = SeedRange.Parse(inputSections[0]);
            var mapSets = inputSections.Skip(1).Select(MapSet.Parse).ToList();
            long destinationToCheck = 0;
            while (true)
            {
                var currentLocation = destinationToCheck;
                foreach (var mapSet in Enumerable.Reverse(mapSets))
                {
                    currentLocation = mapSet.MapDestinationToSource(currentLocation);
                }
                if (seedRanges.Any(seedRange => seedRange.Contains(currentLocation)))
                {
                    return destinationToCheck.ToString();
                }
                destinationToCheck++;
            }
        }
    }
}