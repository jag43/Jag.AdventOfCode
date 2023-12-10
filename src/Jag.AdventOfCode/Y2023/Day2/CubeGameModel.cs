using System;
using System.Collections.Generic;

namespace Jag.AdventOfCode.Y2023.Day2;

public class CubeGameModel
{
    public int Id { get; set; }

    public List<CubeGameRoundModel> Rounds {get;} = new List<CubeGameRoundModel>();

    private const StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static CubeGameModel Parse(string line)
    {
        var model = new CubeGameModel();
        var idRoundsSplit = line.Split(':', stringSplitOptions);
        model.Id = int.Parse(idRoundsSplit[0].Split(' ', stringSplitOptions)[1]);

        var rounds = idRoundsSplit[1].Split(';', stringSplitOptions);
        foreach (var roundString in rounds)
        {
            var round = new CubeGameRoundModel();
            model.Rounds.Add(round);
            var cubeStrings = roundString.Split(',', stringSplitOptions);
            foreach (var cubeString in cubeStrings)
            {
                var cubeStringParts = cubeString.Split(' ', stringSplitOptions);
                switch (cubeStringParts[1])
                {
                    case "red":
                        round.Red = int.Parse(cubeStringParts[0]);
                        break;
                    case "blue":
                        round.Blue = int.Parse(cubeStringParts[0]);
                        break;
                    case "green":
                        round.Green = int.Parse(cubeStringParts[0]);
                        break;
                }
            }
        }

        return model;
    }

    public bool IsPossibleFor(int red, int green, int blue)
    {
        foreach(var round in Rounds)
        {
            if (round.Red > red || round.Green > green || round.Blue > blue)
            {
                return false;
            }
        }

        return true;
    }

    public (int Red, int Blue, int Green) CalculateFewestPossibleCubes()
    {
        int red = 0;
        int blue = 0;
        int green = 0;

        foreach (var round in Rounds)
        {
            if (round.Red.HasValue && round.Red > red)
            {
                red = round.Red.Value;
            }
            if (round.Green.HasValue && round.Green > green)
            {
                green = round.Green.Value;
            }
            if (round.Blue.HasValue && round.Blue > blue)
            {
                blue = round.Blue.Value;
            }
        }

        return (red, blue, green);

    }
}
