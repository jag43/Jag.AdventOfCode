using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jag.AdventOfCode.Y2021.Day20
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 20;

        public string SolvePart1(string input)
        {
            return EnhanceTimes(input, 2);
        }

        public string SolvePart2(string input)
        {
            return EnhanceTimes(input, 50);
        }

        private string EnhanceTimes(string input, int times)
        {
            (bool[] imageAlgorithm, SparseGrid initialGrid) = ParseInput(input);

            SparseGrid current = initialGrid;

            bool border = false;
            for (int i = 0; i < times; i++)
            {
                current = current.EnhanceImage(imageAlgorithm, border);
                switch (border)
                {
                    case true:
                        border = imageAlgorithm[^1];
                        break;
                    case false:
                        border = imageAlgorithm[0];
                        break;
                }
            }

            var count = current.EnumeratePoints().Count().ToString();
            var trueCount = current.EnumeratePoints().Count(((int row, int col, bool value) p) => p.value).ToString();

            return count.ToString();
        }

        public (bool[] imageAlgorithm, SparseGrid initialGrid) ParseInput(string input)
        {
            var chunks = input.Split(Environment.NewLine + Environment.NewLine, SSO.Value);
            bool[] imageAlgorithm = chunks[0].Select(c => c == '.' ? false : c == '#' ? true : throw new Exception("Invalid char in input.")).ToArray();

            var grid = new SparseGrid();
            var gridLines = chunks[1].Split(Environment.NewLine, SSO.Value);
            for (int row = 0; row < gridLines.Length; row++)
            {
                for (int col = 0; col < gridLines[row].Length; col++)
                {
                    char c = gridLines[row][col];
                    bool value = c == '.' ? false : c == '#' ? true : throw new Exception("Invalid char in input.");
                    grid[row, col] = value;
                }
            }

            return (imageAlgorithm, grid);
        }
    }
}