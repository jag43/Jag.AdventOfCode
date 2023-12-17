using System;
using System.Collections.Generic;
using System.Linq;
using Jag.AdventOfCode.Utilities;

namespace Jag.AdventOfCode.Y2023.Day1
{
    public class Solver : ISolver
    {
        public int Year => 2023;

        public int Day => 1;

        private static readonly IReadOnlyDictionary<string, char> digitStrings = new Dictionary<string, char> {
            { "one", '1' },
            { "two", '2' },
            { "three", '3' },
            { "four", '4' },
            { "five", '5' },
            { "six", '6' },
            { "seven", '7' },
            { "eight", '8' },
            { "nine", '9' } }.AsReadOnly();

        public string SolvePart1(string input)
        {
            var sum = 0;

            var lines = input.Split(Environment.NewLine);
            var filteredLines = lines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line
                    .Where(c => c.IsDigit()));
            foreach (var filteredLine in filteredLines)
            {
                var first = filteredLine.First();
                var last = filteredLine.Last();

                sum += int.Parse(new string([first, last]));
            }
            return sum.ToString();
        }

        public string SolvePart2(string input)
        {
            var sum = 0;
            var lines = input.Split(Environment.NewLine);
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));

            foreach (var line in nonEmptyLines)
            {
                var chars = new List<char>();

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i].IsDigit())
                    {
                        chars.Add(line[i]);
                    }
                    string subString = line.SafeSubstring(i, 5);
                    foreach (var digitString in digitStrings)
                    {
                        if (subString.StartsWith(digitString.Key))
                        {
                            // get char for substring
                            chars.Add(digitString.Value);
                            break;
                        }
                    }
                }
                var subSum = new string([chars.First(), chars.Last()]);
                sum += int.Parse(subSum);
            }
            return sum.ToString();
        }
    }
}