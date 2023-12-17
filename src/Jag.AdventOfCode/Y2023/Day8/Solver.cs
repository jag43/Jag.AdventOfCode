using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Jag.AdventOfCode.Utilities;

namespace Jag.AdventOfCode.Y2023.Day8
{
    public class Solver : ISolver
    {
        public int Year => 2023;

        public int Day => 8;

        StringSplitOptions sso = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

        public string SolvePart1(string input)
        {
            var directionsNetworkSplit = input.Split(Environment.NewLine + Environment.NewLine, sso);
            var directions = DirectionRepeater.Parse(directionsNetworkSplit[0]);
            var network = directionsNetworkSplit[1]
                .Split(Environment.NewLine, sso)
                .Select(Node.Parse)
                .ToDictionary(n => n.Name);

            var currentLocation = "AAA";
            long steps = 0;
            foreach (var direction in directions.Repeat())
            {
                currentLocation = direction switch
                {
                    Direction.Right => network[currentLocation].Right,
                    Direction.Left => network[currentLocation].Left,
                    _ => throw new Exception($"Invalid direction: {direction}")
                };
                steps++;
                if (currentLocation == "ZZZ")
                {
                    return steps.ToString();
                }
            }
            throw new Exception("Should not reach here. Expect directions.Repeat() to be infinite.");
        }

        public string SolvePart2(string input)
        {
            var directionsNetworkSplit = input.Split(Environment.NewLine + Environment.NewLine, sso);
            var directions = DirectionRepeater.Parse(directionsNetworkSplit[0]);
            var network = directionsNetworkSplit[1].Split(Environment.NewLine, sso).Select(Node.Parse).ToDictionary(n => n.Name);

            var currentLocations = network.Where(kvp => kvp.Key.EndsWith('A'))
                .Select(kvp => kvp.Key)
                .ToList();
            long steps = 0;

            foreach (var direction in directions.Repeat())
            {
                var nextLocations = new List<string>();
                foreach (var currentLocation in currentLocations)
                {
                    string nextLocation;
                    if (currentLocation.EndsWith('Z'))
                    {
                        nextLocation = steps.ToString();
                    }
                    else if (!network.ContainsKey(currentLocation))
                    {
                        nextLocation = currentLocation;
                    }
                    else 
                    {
                        nextLocation = direction switch
                        {
                            Direction.Right => network[currentLocation].Right,
                            Direction.Left => network[currentLocation].Left,
                            _ => throw new Exception($"Invalid direction: {direction}")
                        };
                    }
                    nextLocations.Add(nextLocation);
                }

                currentLocations = nextLocations;

                steps++;
                if (currentLocations.All(currentLocation => !network.ContainsKey(currentLocation)))
                {
                    return LeastCommonMultiple.Calculate(currentLocations.Select(long.Parse)).ToString();
                }
            }
            throw new Exception("Should not reach here. Expect directions.Repeat() to be infinite.");
        }
    }
}
