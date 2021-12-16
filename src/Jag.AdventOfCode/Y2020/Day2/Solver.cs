using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2020.Day2
{
    public class Solver : ISolver
    {
        public int Year => 2020;

        public int Day => 2;

        public string SolvePart1(string input)
        {
            var rows = ParseInput(input);
            return rows.Count(p => p.IsValid1).ToString();
        }

        public string SolvePart2(string input)
        {
            var rows = ParseInput(input);
            return rows.Count(p => p.IsValid2).ToString();
        }
    
        private IEnumerable<PasswordPolicy> ParseInput(string input)
        {
            foreach(var line in input.Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries))
            {
                var words = line.Split(" ", options: StringSplitOptions.RemoveEmptyEntries);
                var numbers = words[0].Split("-", options: StringSplitOptions.RemoveEmptyEntries);
                yield return new PasswordPolicy()
                {
                    FirstNumber = int.Parse(numbers[0]),
                    SecondNumber = int.Parse(numbers[1]),
                    RequiredChar = words[1][0],
                    Password = words[2]
                };
            }
        }
}
}