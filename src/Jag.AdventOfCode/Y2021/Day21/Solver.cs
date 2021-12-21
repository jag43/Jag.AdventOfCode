using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day21
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 21;

        public string SolvePart1(string input)
        {
            var (p1score, p2score) = ParseInput(input);
            return PlayGame1(p1score, p2score).ToString();
        }

        public string SolvePart2(string input)
        {
            // Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(QuantumRolls.Select(q => new {q.p1roll, q.p2roll, q.universes}), Newtonsoft.Json.Formatting.Indented));
            var (p1score, p2score) = ParseInput(input);
            var (p1wins, p2wins) = PlayGame2(p1score, p2score);
            return new[] { p1wins, p2wins }.Max().ToString();
        }

        private int PlayGame1(int p1Position, int p2Position)
        {
            var totalRolls = 0;
            int p1Score = 0;
            int p2Score = 0;
            int previousRoll = 0;
            while (true)
            {
                var rolls = GetNextRolls(previousRoll).ToList();
                previousRoll = rolls[2];
                p1Position = ((p1Position + rolls.Sum() - 1) % 10) + 1;
                p1Score += p1Position;
                totalRolls += 3;
                if (p1Score >= 1000)
                {
                    return p2Score * totalRolls;
                }
                rolls = GetNextRolls(previousRoll).ToList();
                previousRoll = rolls[2];
                p2Position = ((p2Position + rolls.Sum() - 1) % 10) + 1;
                p2Score += p2Position;
                totalRolls += 3;
                if (p2Score >= 1000)
                {
                    return p1Score * totalRolls;
                }
            }
        }

        private (long P1Wins, long P2Wins) PlayGame2(int p1Position, int p2Position)
        {
            return PlayTurns(1, p1Position, p2Position, 0, 0);
        }

        private Dictionary<(int, int, int, int), (long, long)> Cache { get; } = new();

        private (long P1Wins, long P2Wins) PlayTurns(int turn, int p1PositionInitial, int p2PositionInitial, int p1ScoreInitial, int p2ScoreInitial)
        {
            long p1wins = 0;
            long p2wins = 0;

            foreach (var (p1roll, p2roll, universes) in QuantumRolls)
            {
                var p1Position = p1PositionInitial; 
                var p2Position = p2PositionInitial;
                var p1Score = p1ScoreInitial;
                var p2Score = p2ScoreInitial;

                p1Position = ((p1Position + p1roll - 1) % 10) + 1;
                p1Score += p1Position;
                if (p1Score >= 21)
                {
                    p1wins += universes;
                    continue;
                }
                p2Position = ((p2Position + p2roll - 1) % 10) + 1;
                p2Score += p2Position;
                if (p2Score >= 21)
                {
                    p2wins += universes;
                    continue;
                }
                (long P1Wins, long P2Wins) nextWins;
                if (Cache.TryGetValue((p1Position, p2Position, p1Score, p2Score), out var v))
                {
                    nextWins = v;
                }
                else 
                {
                    nextWins = PlayTurns(turn + 1, p1Position, p2Position, p1Score, p2Score);
                    Cache[(p1Position, p2Position, p1Score, p2Score)] = nextWins;
                }
                p1wins += (universes * nextWins.P1Wins);
                p2wins += (universes * nextWins.P2Wins);
            }

            //Console.WriteLine($"Turn {turn}, p1wins {p1wins}, p2wins {p2wins}, p1score {p1ScoreInitial}, p2score {p2ScoreInitial}");

            return (p1wins, p2wins);
        }

        public static IEnumerable<(int p1roll, int p2roll, int universes)> QuantumRolls { get; } = GetQuantumRolls().ToList();

        private static IEnumerable<(int p1roll, int p2roll, int universes)> GetQuantumRolls()
        {
            List<int> scores = new();
            var numbers = new [] { 1, 2, 3 };
            foreach (var one in numbers)
            {
                foreach (var two in numbers)
                {
                    foreach (var three in numbers)
                    {
                        scores.Add(one + two + three);
                    }
                }
            }

            List<(int PlayerOneScore, int PlayerTwoScore)> playerScores = new();
            foreach (var p1score in scores)
            {
                foreach (var p2score in scores)
                {
                    playerScores.Add((p1score, p2score));
                }
            }

            return playerScores.GroupBy(s => s).Select(g => (g.Key.PlayerOneScore, g.Key.PlayerTwoScore, g.Count()));
        }

        public IEnumerable<int> GetNextRolls(int previous)
        {
            yield return ((++previous - 1) % 100) + 1;
            yield return ((++previous - 1) % 100) + 1;
            yield return ((++previous - 1) % 100) + 1;
        }

        public (int P1Start, int P2Start) ParseInput(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var p1start = int.Parse(lines[0].Substring(lines[0].Length - 2));
            var p2start = int.Parse(lines[1].Substring(lines[1].Length - 2));

            return (p1start, p2start);
        }

    }
}