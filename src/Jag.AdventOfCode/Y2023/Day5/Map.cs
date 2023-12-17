using System;
using System.ComponentModel;

namespace Jag.AdventOfCode.Y2023.Day5;

public class Map
{
    public Map(long sourceStart, long destinationStart, long rangeLength)
    {
        SourceStart = sourceStart;
        DestinationStart = destinationStart;
        RangeLength = rangeLength;

        SourceEnd = SourceStart + RangeLength;
        DestinationEnd = DestinationStart + RangeLength;
    }

    private const StringSplitOptions sso = SSO.Value;
    
    public long SourceStart { get; }
    
    public long SourceEnd { get; }
    
    public long DestinationStart { get; }
    
    public long DestinationEnd { get; }

    public long RangeLength { get; }

    public bool Contains(long currentLocation)
    {
        return currentLocation >= SourceStart && currentLocation < SourceEnd;
    }

    public bool DestinationContains(long currentLocation)
    {
        return currentLocation >= DestinationStart && currentLocation < DestinationEnd;
    }

    public long MapSourceToDestination(long currentLocation)
    {
        if (!Contains(currentLocation))
        {
            throw new InvalidOperationException("Cannot map, location not in source range");
        }
        var offset = currentLocation - SourceStart;
        return DestinationStart + offset;
    }

    public long MapDestinationToSource(long currentLocation)
    {
        if (!DestinationContains(currentLocation))
        {
            throw new InvalidOperationException("Cannot map, location not in destination range");
        }
        var offset = currentLocation - DestinationStart;
        return SourceStart + offset;
    }

    public static Map Parse(string line)
    {
        var numbers = line.Split(' ', sso);
        long destinationStart = long.Parse(numbers[0]);
        long sourceStart = long.Parse(numbers[1]);
        long rangeLength = long.Parse(numbers[2]);

        return new Map(sourceStart, destinationStart, rangeLength);
    }
}