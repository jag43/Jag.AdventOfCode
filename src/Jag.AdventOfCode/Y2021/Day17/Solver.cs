using System;
using System.Linq;
using System.Text.RegularExpressions;
using Jag.AdventOfCode.Utilities;

namespace Jag.AdventOfCode.Y2021.Day17
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 17;

        public string SolvePart1(string input)
        {
            var ((xMin, xMax), (yMin, yMax)) = ParseInput(input);

            var initialXVelocty = TriangleNumbers.ReverseTriangleNumber(xMax);
            var maxYVelocity = Enumerable.Range(xMin, xMax)
                .Select(xTarget => CalculateMaxYVelocity(yMin, yMax))
                .Max();

            return  TriangleNumbers.TriangleNumber(maxYVelocity).ToString();
        }

        public string SolvePart2(string input)
        {
            var ((xMin, xMax), (yMin, yMax)) = ParseInput(input);

            var minXVelocity = 1;
            var maxXVelocity = xMax;

            var minYVelocity = yMin;
            var yMaxVelocity = Enumerable.Range(xMin, xMax)
                .Select(xTarget => CalculateMaxYVelocity(yMin, yMax))
                .Max();

            int validCombos = 0;
            for (int y = minYVelocity; y <= yMaxVelocity; y++)
            {
                for (int x = minXVelocity; x <= maxXVelocity; x++)
                {
                    if (StepsToTargetArea(x, y, xMin, xMax, yMin, yMax).Success == true)
                    {
                        validCombos++;
                    }
                }
            }

            return validCombos.ToString();
        }

        private int CalculateMaxYVelocity(int yMin, int yMax)
        {
            return Math.Max(yMax, Math.Abs(yMin) - 1);
        }

        private (int Steps, bool Success) StepsToTargetArea(int xVelocity, int yVelocity, int xMin, int xMax, int yMin, int yMax)
        {
            int currentXVelocity = xVelocity;
            int currentYVelocity = yVelocity;
            int x = 0;
            int y = 0;
            for (int step = 1; ; step++)
            {
                x += currentXVelocity;
                y += currentYVelocity;
                if (x >= xMin & x <= xMax && y >= yMin & y <= yMax)
                {
                    return (step, true);
                }
                else if (y < yMin || x > xMax || (currentXVelocity == 0 && x < xMin) || (currentXVelocity == 0 && x > xMax))
                {
                    return (step, false);
                }
                switch (currentXVelocity)
                {
                    case > 0:
                        currentXVelocity--;
                        break;
                    case < 0:
                        currentXVelocity++;
                        break;
                }
                currentYVelocity--;
            }
            throw new Exception();
        }

        public ((int Min, int Max) X, (int Min, int Max) Y) ParseInput(string input)
        {
            var match = Regex.Match(input, "^target area: x=(?<x1>[0-9-]*)..(?<x2>[0-9-]*), y=(?<y1>[0-9-]*)..(?<y2>[0-9-]*)");

            var x1 = int.Parse(match.Groups["x1"].Value);
            var x2 = int.Parse(match.Groups["x2"].Value);
            var y1 = int.Parse(match.Groups["y1"].Value);
            var y2 = int.Parse(match.Groups["y2"].Value);

            var xMin = Math.Min(x1, x2);
            var xMax = Math.Max(x1, x2);

            var yMin = Math.Min(y1, y2);
            var yMax = Math.Max(y1, y2);

            return ((xMin, xMax), (yMin, yMax));
        }
    }
}