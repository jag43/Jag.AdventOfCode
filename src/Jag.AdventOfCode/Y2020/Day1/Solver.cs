using System;
using System.Linq;

namespace Jag.AdventOfCode.Y2020.Day1
{
    public class Solver : ISolver
    {
        public int Year => 2020;

        public int Day => 1;

        public string SolvePart1(string input)
        {
            int[] numbers = ParseInput(input);

            int searchFor = 2020;
            foreach (var i in numbers)
            {
                foreach (var k in numbers)
                {
                    if (i + k == searchFor)
                    {
                        return (i * k).ToString();
                    }
                }
            }

            throw new Exception();
        }

        public string SolvePart2(string input)
        {
                        int[] numbers = ParseInput(input);

            int searchFor = 2020;
            foreach (var i in numbers)
            {
                foreach (var k in numbers)
                {
                    foreach (var q in numbers)
                    {
                        if (i + k + q == searchFor)
                        {
                            return (i * k * q).ToString();
                        }
                    }
                }
            }

            throw new Exception();
        }

        private int[] ParseInput(string input)
        {
            return input.Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .ToArray();
        }
    }
}