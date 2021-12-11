using System;

namespace Jag.AdventOfCode
{
    public interface ISolver
    {
        int Year { get; }

        int Day { get; }

        string SolvePart1(string input);

        string SolvePart2(string input);

        string SolvePart(string input, int part)
        {
            return part switch
            {
                1 => SolvePart1(input),
                2 => SolvePart2(input),
                _ => throw new Exception($"Invalid part {part}")
            };
        }
    }
}