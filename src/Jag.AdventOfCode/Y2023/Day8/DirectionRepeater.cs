using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2023.Day8;

public class DirectionRepeater(List<Direction> directions)
{
    public IEnumerable<Direction> Repeat()
    {
        while (true)
        {
            foreach (Direction direction in directions)
            {
                yield return direction;
            }
        }
    }

    public static DirectionRepeater Parse(string line)
    {
        var list = line.Select(c => c switch
        {
            'R' => Direction.Right,
            'L' => Direction.Left,
            _ => throw new Exception($"Invalid direction: {c}")
        })
        .ToList();

        return new DirectionRepeater(list);
    }
}