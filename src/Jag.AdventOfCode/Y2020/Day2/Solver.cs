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
            return rows.Count(p => p.IsValid).ToString();

        }

        public string SolvePart2(string input)
        {
            throw new NotImplementedException();
        }
    
        private IEnumerable<PasswordPolicy> ParseInput(string input)
        {
            foreach(var line in input.Split(Environment.NewLine))
            {
                yield return new PasswordPolicy()
                {
                    Min = int.Parse(line[0].ToString()),
                    Max = int.Parse(line[2].ToString()),
                    RequiredChar = line[4],
                    Password = line.Substring(7)
                };
            }
        }
}
}