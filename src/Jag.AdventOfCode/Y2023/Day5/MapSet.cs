using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2023.Day5;

public class MapSet
{
    private const StringSplitOptions sso = SSO.Value;
    public string Name { get; set; }
    public List<Map> Maps { get; set; } = [];

    public static MapSet Parse(string inputSection)
    {
        var lines = inputSection.Split(Environment.NewLine, sso);
        var name = lines[0].Split(' ', sso)[0];
        var maps = lines.Skip(1).Select(Map.Parse);
        return new MapSet()
        {
            Name = name,
            Maps = maps.ToList()
        };
    }

    public long MapSourceToDestination(long currentLocation)
    {
        foreach (var map in Maps)
        {
            if (map.Contains(currentLocation))
            {
                return map.MapSourceToDestination(currentLocation);
            }
        }

        return currentLocation;
    }
    public long MapDestinationToSource(long currentLocation)
    {
        foreach (var map in Maps)
        {
            if (map.DestinationContains(currentLocation))
            {
                return map.MapDestinationToSource(currentLocation);
            }
        }

        return currentLocation;
    }
}
