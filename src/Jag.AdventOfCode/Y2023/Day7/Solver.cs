using System;
using System.Buffers;
using System.Linq;

namespace Jag.AdventOfCode.Y2023.Day7
{
    public class Solver : ISolver
    {
        public int Year => 2023;

        public int Day => 7;

        public string SolvePart1(string input)
        {
            var games = input.Split(Environment.NewLine, SSO.Value)
                .Select(Game1.Parse)
                .ToArray();
            Array.Sort(games);
            games = games.Reverse().ToArray();
            long totalWinnings = 0;
            for (int i = 0; i < games.Length; i++)
            {
                var game = games[i];
                var winnings = game.Bid * (i + 1);
                totalWinnings += winnings;
            }
            return totalWinnings.ToString();
        }

        public string SolvePart2(string input)
        {
            var games = input.Split(Environment.NewLine, SSO.Value)
                .Select(Game2.Parse)
                .ToArray();
            Array.Sort(games);
            games = games.Reverse().ToArray();
            long totalWinnings = 0;
            for (int i = 0; i < games.Length; i++)
            {
                var game = games[i];
                var winnings = game.Bid * (i + 1);
                totalWinnings += winnings;
            }
            return totalWinnings.ToString();
        }
    }
}