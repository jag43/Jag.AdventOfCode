using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2023.Day5;

public class SeedRange
{
    public SeedRange(long rangeStart, long rangeLength)
    {
        RangeStart = rangeStart;
        RangeLength = rangeLength;
        RangeEnd = rangeStart + rangeLength;
    }

    public long RangeStart { get; }

    public long RangeLength { get; }

    public long RangeEnd { get; }

    public static List<SeedRange> Parse(string line)
    {
        var list = new List<SeedRange>();
        var numbers = line.Split(' ', SSO.Value)
            .Skip(1)
            .Select(long.Parse)
            .ToList();
        for (int i = 0; i < numbers.Count - 1; i += 2)
        {
            list.Add(new SeedRange(numbers[i], numbers[i + 1]));
        }
        return list;
    }

    public bool Contains(long location)
    {
        return location >= RangeStart && location < RangeEnd;
    }
}