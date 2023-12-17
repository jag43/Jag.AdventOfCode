using System;
using System.Collections.Generic;
using System.Linq;
using Jag.AdventOfCode.Utilities;

namespace Jag.AdventOfCode.Y2023.Day3
{
    public class Solver : ISolver
    {
        public int Year => 2023;

        public int Day => 3;

        public string SolvePart1(string input)
        {
            var lines = input.Split(Environment.NewLine, SSO.Value);
            var partsNumbers = new List<int>();
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                var line = lines[lineIndex];
                for (int charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    if (charIndex > 0 && line[charIndex - 1].IsDigit())
                    {
                        continue;
                    }
                    if (!line[charIndex].IsDigit())
                    {
                        continue;
                    }
                    var number = BuildNumber(line, charIndex);
                    if (NumberHasSymbolAdjacentOrDiagonal(number, lineIndex, charIndex, lines))
                    {
                        partsNumbers.Add(int.Parse(number));
                    }
                }
            }
            return partsNumbers.Sum().ToString();
        }

        private string BuildNumber(string line, int startIndex)
        {
            List<char> chars = [line[startIndex]];
            for (int nextIndex = startIndex + 1; nextIndex < line.Length && line[nextIndex].IsDigit(); nextIndex++)
            {
                chars.Add(line[nextIndex]);
            }
            return new string(chars.ToArray());
        }

        private bool NumberHasSymbolAdjacentOrDiagonal(string number, int lineIndex, int charIndex, string[] lines)
        {
            if (lineIndex > 0)
            {
                // check previous line for symbol
                if (LineHasSymbolBetweenPositions(lines[lineIndex - 1], charIndex, number.Length))
                {
                    return true;
                }
            }
            // check current line for symbol
            if (LineHasSymbolBetweenPositions(lines[lineIndex], charIndex, number.Length))
            {
                return true;
            }

            if (lineIndex + 1 < lines.Length)
            {
                // check subsequent line for symbol
                if (LineHasSymbolBetweenPositions(lines[lineIndex + 1], charIndex, number.Length))
                {
                    return true;
                }
            }

            return false;
        }

        private bool LineHasSymbolBetweenPositions(string line, int numberStartsAtIndex, int numberLength)
        {
            var checkStartsAtIndex = Math.Max(numberStartsAtIndex - 1, 0);
            var checkEndsAtIndex = Math.Min(numberStartsAtIndex + numberLength + 1, line.Length - 1);
            for (int i = checkStartsAtIndex; i < checkEndsAtIndex; i++)
            {
                char c = line[i];
                if (!c.IsDigit() && c != '.' && c != '\r' && c != '\n')
                {
                    return true;
                }
            }
            return false;
        }

        public string SolvePart2(string input)
        {
            var lines = input.Split(Environment.NewLine, SSO.Value);
            var (asterisks, partNumbers) = BuildAsteriskAndPartNumbers(lines);
            var gears = new List<(int, int)>();
            foreach (var asterisk in asterisks)
            {
                var adjAndDiagCoordinates = GetAdjacentAndDiagonalPositions(asterisk, lines[0].Length, lines.Length);
                var adjOrDiagPartsNumbers = new HashSet<(Guid, string)>();
                foreach (var adjOrDiagCoordinate in adjAndDiagCoordinates)
                {
                    if (partNumbers.ContainsKey(adjOrDiagCoordinate))
                    {
                        adjOrDiagPartsNumbers.Add(partNumbers[adjOrDiagCoordinate]);
                    }
                    if (adjOrDiagPartsNumbers.Count > 2)
                    {
                        break;
                    }
                }
                if (adjOrDiagPartsNumbers.Count == 2)
                {
                    int part1 = int.Parse(adjOrDiagPartsNumbers.First().Item2);
                    int part2 = int.Parse(adjOrDiagPartsNumbers.Last().Item2);
                    gears.Add((part1, part2));
                }
            }
            return gears.Sum(t => t.Item1 * t.Item2).ToString();
        }

        private (List<Coordinate> Asterisks, Dictionary<Coordinate, (Guid, string)> PartNumbers) BuildAsteriskAndPartNumbers(string[] lines)
        {
            var asterisks = new List<Coordinate>();
            var partNumbers = new Dictionary<Coordinate, (Guid, string)>();
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                var line = lines[lineIndex];
                for (int charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    var coordinate = new Coordinate(charIndex, lineIndex);
                    if (line[charIndex] == '*')
                    {
                        asterisks.Add(coordinate);
                    }

                    if (line[charIndex].IsDigit())
                    {
                        var firstChar = charIndex == 0;
                        var prevCharIsDigit = charIndex > 0 && line[charIndex - 1].IsDigit();
                        if (firstChar || !prevCharIsDigit)
                        {
                            partNumbers.Add(coordinate, (Guid.NewGuid(), BuildNumber(line, charIndex)));
                        }
                        else // then prev char must be digit
                        {
                            partNumbers.Add(coordinate, partNumbers[coordinate with { X = charIndex - 1 }]);
                        }
                    }
                }
            }
            return (asterisks, partNumbers);
        }
    
        private static IEnumerable<Coordinate> GetAdjacentAndDiagonalPositions(Coordinate center, int xBound, int yBound)
        {
            var yOffsets = new int[] { -1, 0, 1 };
            var xOffsets = new int[] { -1, 0, 1 };

            foreach (var xOffset in xOffsets)
            {
                foreach (int yOffset in yOffsets)
                {
                    var neighbourToCheck = new Coordinate(center.X + xOffset, center.Y + yOffset);
                    if (neighbourToCheck.X < 0 || neighbourToCheck.X >= xBound || neighbourToCheck.Y < 0 || neighbourToCheck.Y >= yBound)
                    {
                        continue;
                    }
                    yield return neighbourToCheck;
                }
            }
        }
    }
    public record Coordinate(int X, int Y);
}