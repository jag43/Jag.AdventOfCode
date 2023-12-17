namespace Jag.AdventOfCode.Y2023.Day6;

using System;
using System.Collections.Generic;
using System.Linq;

public class Solver : ISolver
{
    public int Year => 2023;

    public int Day => 6;

    public string SolvePart1(string input)
    {
        var races = Race.ParseRaces(input);
        List<int> numberOfWaysToWinList = new();
        foreach (var race in races)
        {
            int numberOfWaysToWin = 0;
            for (int timeHoldingButton = 1; timeHoldingButton < race.Time; timeHoldingButton++)
            {
                long speed = timeHoldingButton;
                var distance = (race.Time - timeHoldingButton) * speed;
                if (distance > race.Distance)
                {
                    numberOfWaysToWin++;
                }
            }
            numberOfWaysToWinList.Add(numberOfWaysToWin);
        }
        return numberOfWaysToWinList.Aggregate(1L, (prev, next) => prev * next).ToString();
    }

    public string SolvePart2(string input)
    {
        var race = Race.ParseRace(input);
        
        int numberOfWaysToWin = 0;
        for (int timeHoldingButton = 1; timeHoldingButton < race.Time; timeHoldingButton++)
        {
            long speed = timeHoldingButton;
            var distance = (race.Time - timeHoldingButton) * speed;
            if (distance > race.Distance)
            {
                numberOfWaysToWin++;
            }
        }
        return numberOfWaysToWin.ToString();
    }
}

public record Race(long Time, long Distance) 
{
    private const StringSplitOptions sso = SSO.Value;
   
    public static List<Race> ParseRaces(string input)
    {
        var lines = input.Split(Environment.NewLine, sso);
        var times = lines[0].Split(' ', sso).Skip(1).Select(long.Parse);
        var distances = lines[1].Split(' ', sso).Skip(1).Select(long.Parse);
        return times.Zip(distances, (time, distance) => new Race(time, distance)).ToList();
    }
    public static Race ParseRace(string input)
    {
        var lines = input.Split(Environment.NewLine, sso);
        var time = lines[0].Replace(" ", "").Split(':', sso).Skip(1).Select(long.Parse).Single();
        var distance = lines[1].Replace(" ", "").Split(':', sso).Skip(1).Select(long.Parse).Single();
        return new Race(time, distance);
    }
}