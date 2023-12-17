using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2023.Day4;

public class Card 
{
    private const StringSplitOptions stringSplitOptions = SSO.Value;

    public int Id { get; set; }

    public HashSet<int> WinningNumbers { get; set; } = new();

    public List<int> Numbers { get; set; } = new();

    public int Copies { get; set; } = 1;

    public int NumberOfWinningNumberMatches => Numbers.Count(WinningNumbers.Contains);

    public static Card Parse(string line)
    {
        var idNumbersSplit = line.Split(':', stringSplitOptions);
        var idString = idNumbersSplit[0].Split(' ', stringSplitOptions)[1];
        var id = int.Parse(idString);
        var numbersSplit = idNumbersSplit[1].Split('|', stringSplitOptions);
        var winningNumberStrings = numbersSplit[0].Split(' ', stringSplitOptions);
        var numberStrings = numbersSplit[1].Split(' ', stringSplitOptions);

        return new Card()
        {
            Id = id,
            WinningNumbers = winningNumberStrings.Select(int.Parse).ToHashSet(),
            Numbers = numberStrings.Select(int.Parse).ToList()
        };
    }
}