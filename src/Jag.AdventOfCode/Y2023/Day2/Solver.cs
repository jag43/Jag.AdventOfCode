using System;
using System.Linq;

namespace Jag.AdventOfCode.Y2023.Day2
{
    public class Solver : ISolver
    {
        public int Year => 2023;

        public int Day => 2;

        public string SolvePart1(string input)
        {
            // challenge: only 12 red cubes, 13 green cubes, and 14 blue cubes
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var games = lines.Select(CubeGameModel.Parse).ToList();
            var possibleGames = games.Where(game => game.IsPossibleFor(12, 13, 14));
            return possibleGames.Sum(games => games.Id).ToString();
        }

        public string SolvePart2(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var games = lines.Select(CubeGameModel.Parse).ToList();
            var fewestPossibleCubes = games.Select(game => game.CalculateFewestPossibleCubes());
            return fewestPossibleCubes.Sum(fpc => fpc.Red * fpc.Green * fpc.Blue).ToString();
        }
    }
}