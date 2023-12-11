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
            // foreach (var group in input.GroupBy(c => c).OrderBy(group => group.Count()))
            // {
            //     Console.WriteLine($"{group.Key}: {group.Count()}");
            // }
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
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
            // Console.WriteLine($"{line}: Check {checkStartsAtIndex}-{checkEndsAtIndex}");
            for (int i = checkStartsAtIndex; i <= checkEndsAtIndex; i++)
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
            throw new NotImplementedException();
        }
    }
}