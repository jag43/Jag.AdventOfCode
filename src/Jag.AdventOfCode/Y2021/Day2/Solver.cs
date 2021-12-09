using System;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day2
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 2;

        public string SolvePart1(string input)
        {
            var commands = input.Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(" "));

            var horizontal = 0;
            var depth = 0;

            foreach (var command in commands)
            {
                switch (command[0])
                {
                    case "forward":
                        horizontal += int.Parse(command[1]);
                        break;
                    case "up":
                        depth -= int.Parse(command[1]);
                        break;
                    case "down":
                        depth += int.Parse(command[1]);
                        break;
                }
            }

            return (horizontal * depth).ToString();
        }

        public string SolvePart2(string input)
        {
                        var commands = input.Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(" "));

            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            foreach (var command in commands)
            {
                var unit = int.Parse(command[1]);
                switch (command[0])
                {
                    case "forward":
                        horizontal += unit;
                        depth += aim * unit;
                        break;
                    case "up":
                        aim -= unit;
                        break;
                    case "down":
                        aim += unit;
                        break;
                }
            }

            return (horizontal * depth).ToString();
        }
    }
}