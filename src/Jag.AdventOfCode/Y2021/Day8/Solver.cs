using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day8
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 8;

        public string SolvePart1(string input)
        {
            var lines = ParseInput(input);

            var numberOfTimesRelevantDigitsAppear = 0;

            var l = BuildScrambledSegmentLookup(SevenSegmentDisplay.Numbers.ToArray());

            foreach (var line in lines)
            {
                var lookup = BuildScrambledSegmentLookup(line.Left);
                var relevantDigits = new HashSet<string> { lookup[SevenSegmentDisplay.One], lookup[SevenSegmentDisplay.Four], lookup[SevenSegmentDisplay.Seven], lookup[SevenSegmentDisplay.Eight] };
                foreach (var scrambledSegment in line.Right)
                {
                    if (relevantDigits.Contains(scrambledSegment))
                    {
                        numberOfTimesRelevantDigitsAppear++;
                    }
                }
            }
            
            return numberOfTimesRelevantDigitsAppear.ToString();
        }

        public string SolvePart2(string input)
        {
             var lines = ParseInput(input);

            var fourDigitCodes = new List<int>();

            foreach (var line in lines)
            {
                var lookup = BuildScrambledSegmentLookup(line.Left).ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
                var one = lookup[line.Right[0]];
                var two = lookup[line.Right[1]];
                var three = lookup[line.Right[2]];
                var four = lookup[line.Right[3]];

                fourDigitCodes.Add(SevenSegmentDisplay.ConvertSegmentsToInt(one, two, three, four));
            }
            
            return fourDigitCodes.Sum().ToString();
        }

        private Dictionary<string, string> BuildScrambledSegmentLookup(string[] scrambledSegments)
        {
            var lookup = new Dictionary<string, string>();
            var remainingScrambledSegments = scrambledSegments.Where(s => !lookup.ContainsValue(s));

            lookup.Add(SevenSegmentDisplay.One, scrambledSegments.Single(s => s.Length == 2));
            lookup.Add(SevenSegmentDisplay.Seven, scrambledSegments.Single(s => s.Length == 3));
            lookup.Add(SevenSegmentDisplay.Four, scrambledSegments.Single(s => s.Length == 4));
            lookup.Add(SevenSegmentDisplay.Eight, scrambledSegments.Single(s => s.Length == 7));

            var segmentFrequency = SevenSegmentDisplay.BuildSegmentFrequency(scrambledSegments);
            var e = segmentFrequency.Single(sf => sf.Value == 4).Key;
            lookup.Add(SevenSegmentDisplay.Two, scrambledSegments.Single(s => s.Length == 5 && s.Contains(e)));
            lookup.Add(SevenSegmentDisplay.Six, remainingScrambledSegments.Single(s => s.Length == 6
                && 
                    (
                        (s.Contains(lookup[SevenSegmentDisplay.One][0]) && !s.Contains(lookup[SevenSegmentDisplay.One][1]))
                        || (!s.Contains(lookup[SevenSegmentDisplay.One][0]) && s.Contains(lookup[SevenSegmentDisplay.One][1])))));
            var remainingSegmentFrequency = SevenSegmentDisplay.BuildSegmentFrequency(remainingScrambledSegments);
            lookup.Add(SevenSegmentDisplay.Zero, remainingScrambledSegments.Single(s => s.Length == 6 && s.Any(c => remainingSegmentFrequency[c] == 1)));
            lookup.Add(SevenSegmentDisplay.Nine, remainingScrambledSegments.Single(s => s.Length == 6));
            // FIVE AND THREE
            var cf = lookup[SevenSegmentDisplay.One];
            lookup.Add(SevenSegmentDisplay.Three, remainingScrambledSegments.Single(s => s.Length == 5 && s.Intersect(cf).Count() == 2));
            lookup.Add(SevenSegmentDisplay.Five, remainingScrambledSegments.Single());

            return lookup;//.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
        }

        private IEnumerable<(string[] Left, string[] Right)> ParseInput(string input)
        {
            var lines = input.Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(" | ", options: StringSplitOptions.RemoveEmptyEntries));
            foreach (var line in lines)
            {
                var left = line[0].Split(" ").Select(segment => new string(new SortedSet<char>(segment).ToArray())).ToArray();
                var right = line[1].Split(" ").Select(segment => new string(new SortedSet<char>(segment).ToArray())).ToArray();
                yield return (left, right);
            }

        }
    }
}