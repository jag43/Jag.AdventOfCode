using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day13
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 13;

        public string SolvePart1(string input)
        {
            var foldedPaper = Solve(input, 1);
            return foldedPaper.SelectMany(b => b).Sum(b => b ? 1 : 0).ToString();
        }

        public string SolvePart2(string input)
        {
            var foldedPaper = Solve(input);
            return Environment.NewLine + string.Join(Environment.NewLine, foldedPaper.Select(array => new string(array.Select(b => b ? '#' : ' ').ToArray())));
        }

        private bool[][] Solve(string input, int? numFolds = null)
        {
            var (points, folds) = ParseInput(input);
            var maxX = points.Max(p => p.x) + 1;
            var maxY = points.Max(p => p.y) + 1;

            var paper = Enumerable.Repeat(0, maxY)
                .Select(_ => new bool[maxX])
                .ToArray();

            foreach (var (x, y) in points)
            {
                paper[y][x] = true;
            }
            
            var foldsTouse = numFolds.HasValue ? folds.Take(numFolds.Value) : folds;

            foreach (var (axis, index) in foldsTouse)
            {
                paper = FoldPaper(paper, axis, index);
            }

            return paper;
        }

        private bool[][] FoldPaper(bool[][] paper, char axis, int index)
        {
            switch (axis)
            {
                case 'x':
                    for (int f = 1; index + f < paper[0].Length; f++)
                    {
                        var foldI = index + f;
                        var newI = index - f;
                        for (int k = 0; k < paper.Length; k++)
                        {
                            paper[k][newI] |= paper[k][foldI];
                        }
                    }
                    return paper.Select(array => array.Take(index).ToArray()).ToArray();
                case 'y':
                    for (int f = 1; index + f < paper.Length; f++)
                    {
                        var foldK = index + f;
                        var newK = index - f;
                        for (int i = 0; i < paper[0].Length; i++)
                        {
                            paper[newK][i] |= paper[foldK][i];
                        }
                    }
                    return paper.Take(index).ToArray();
                default : 
                    throw new Exception();
            }
        }

        public (List<(int x, int y)> Points, List<(char Axis, int Index)> Folds) ParseInput(string input)
        {
            var pointsAndFolds = input.Split(Environment.NewLine + Environment.NewLine);
            var points = pointsAndFolds[0].Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries)
                .Select(s => 
                {
                    var xy = s.Split(',', options: StringSplitOptions.RemoveEmptyEntries);
                    return (int.Parse(xy[0]), int.Parse(xy[1]));
                })
                .ToList();
            var folds = pointsAndFolds[1].Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries)
                .Select(s => (s[11], int.Parse(s.Substring(13))))
                .ToList();

                return (points, folds);
        }
    }
}