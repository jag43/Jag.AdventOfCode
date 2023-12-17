using System;
using System.Diagnostics;

namespace Jag.AdventOfCode.Y2023.Day7;

[DebuggerDisplay("{Hand} {Bid}")]
public record Game2 (Hand2 Hand, long Bid) : IComparable<Game2>
{
    private string DebuggerDisplay => $"{Hand.HandType} {Hand.Card1.ToLabel()} {Hand.Card2.ToLabel()} {Hand.Card3.ToLabel()} {Hand.Card4.ToLabel()} {Hand.Card5.ToLabel()} {Bid}";

    public int CompareTo(Game2 other)
    {
        return this.Hand.CompareTo(other.Hand);
    }

    public override int GetHashCode()
    {
        return Hand.GetHashCode();
    }

    public static Game2 Parse(string line)
    {
        var split = line.Split(" ", SSO.Value);
        return new Game2(Hand2.Parse(split[0]), long.Parse(split[1]));
    }
}