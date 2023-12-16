using System;
using System.Diagnostics;

namespace Jag.AdventOfCode.Y2023.Day7;

[DebuggerDisplay("{Hand} {Bid}")]
public record Game1 (Hand1 Hand, long Bid) : IComparable<Game1>
{
    private string DebuggerDisplay => $"{Hand.HandType} {Hand.Card1.ToLabel()} {Hand.Card2.ToLabel()} {Hand.Card3.ToLabel()} {Hand.Card4.ToLabel()} {Hand.Card5.ToLabel()} {Bid}";

    public int CompareTo(Game1 other)
    {
        return this.Hand.CompareTo(other.Hand);
    }

    public override int GetHashCode()
    {
        return Hand.GetHashCode();
    }

    public static Game1 Parse(string line)
    {
        var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return new Game1(Hand1.Parse(split[0]), long.Parse(split[1]));
    }
}