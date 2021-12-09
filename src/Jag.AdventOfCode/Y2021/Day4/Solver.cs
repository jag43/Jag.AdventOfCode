using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jag.AdventOfCode;
using Newtonsoft.Json;

namespace Jag.AdventOfCode.Y2021.Day4
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 4;

        public string SolvePart1(string input)
        {
            var bingoData = BingoData.CreateFromString(input);

            BingoBoard winner = null;
            foreach(var board in bingoData.Boards)
            {
                if (winner == null || winner.AchievesBingo() > board.AchievesBingo())
                {
                    winner = board;
                }
            }
            
           return winner.GetScore().ToString();
        }

        public string SolvePart2(string input)
        {
            var bingoData = BingoData.CreateFromString(input);

            BingoBoard loser = null;
            foreach(var board in bingoData.Boards)
            {
                if (loser == null || loser.AchievesBingo() < board.AchievesBingo())
                {
                    loser = board;
                }
            }
            
           return loser.GetScore().ToString();
        }
    }
}
