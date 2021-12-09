using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day4
{
    public class BingoBoard
    {
        private readonly BingoData bingoData;

        public BingoBoard(BingoData bingoData)
        {
            this.bingoData = bingoData;
        }

        public List<string> RawBoard { get; } = new();

        public List<List<BingoSquare>> Rows { get; } = new();

        public List<List<BingoSquare>> Columns { get; } = new();

        public int AchievesBingo() => Rows.Concat(Columns).Min(squares => squares.AchievesBingo());

        public int GetScore()
        {
            var achievesBingo = this.AchievesBingo();
            var lastBingoNumber = bingoData.Numbers[achievesBingo];
            var allSquares = this.Rows.SelectMany(row => row);
            var allUnmarked = allSquares.Where(square => square.Selected > achievesBingo);
            var sum = allUnmarked.Sum(sq => sq.Number);

            return sum * lastBingoNumber;
        }

        public static BingoBoard CreateFrom(BingoData bingoData, List<string> rows)
        {
            var board = new BingoBoard(bingoData);            
            board.RawBoard.AddRange(rows);

            // populate rows
            foreach (var row in rows)
            {
                var squares = row.Split(" ")
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(n => new BingoSquare(bingoData)
                    {
                        Number = int.Parse(n)
                    }).ToList();
                board.Rows.Add(squares);
            }

            // build columns
            for (int i = 0; i < board.Rows[0].Count; i++)
            {
                var column = new List<BingoSquare>();
                for (int k = 0; k < board.Rows.Count; k++)
                {
                    column.Add(board.Rows[k][i]);
                }
                board.Columns.Add(column);
            }

            return board;
        }
    }
}