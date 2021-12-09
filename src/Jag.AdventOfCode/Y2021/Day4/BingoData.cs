using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day4
{
    public class BingoData
    {
        public List<int> Numbers { get; } = new List<int>();

        public List<BingoBoard> Boards { get; } = new List<BingoBoard>();

        public static BingoData CreateFromString(string input)
        {
            var bingoData = new BingoData();

            var lines = input.Split(Environment.NewLine).ToList();
            bingoData.Numbers.AddRange(lines[0].Split(",").Select(s => int.Parse(s)));

            lines.RemoveRange(0, 2);

            var boards = GetBoardsRaw(lines)
                .Select(board => BingoBoard.CreateFrom(bingoData, board));
            bingoData.Boards.AddRange(boards);

            return bingoData;
        }

        private static IEnumerable<List<string>> GetBoardsRaw(List<string> lines)
        {
            var list = new List<string>();
            foreach (var line in lines)
            {
                if (line == string.Empty)
                {
                    yield return list;
                    list = new();
                }
                else
                {
                    list.Add(line);
                }
            }
            if (list.Any())
            {
                yield return list;
            }
        }
    }
}