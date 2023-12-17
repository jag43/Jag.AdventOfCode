using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day10
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 10;

        private Dictionary<char, int> PartOneScores = new Dictionary<char, int>
        {
            {')', 3},
            {']', 57},
            {'}', 1197},
            {'>', 25137}
        };

        private Dictionary<char, long> PartTwoScores = new Dictionary<char, long>
        {
            { ')', 1L },
            { ']', 2L },
            { '}', 3L },
            { '>', 4L }
        };

        private Dictionary<char, char> Matches = new Dictionary<char, char>
        {
            {'(', ')' },
            {'[', ']' },
            {'{', '}' },
            {'<', '>' }
        };

        private HashSet<char> OpeningBraces = new HashSet<char> { '(', '[', '{', '<' };

        private HashSet<char> ClosingBraces = new HashSet<char> { ')', ']', '}', '>' };

        public string SolvePart1(string input)
        {
            string[] lines = ParseInput(input);
            int score = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                var stack = new Stack<char>();
                foreach (var ch in lines[i])
                {
                    if (OpeningBraces.Contains(ch))
                    {
                        stack.Push(ch);
                    }
                    else if (ClosingBraces.Contains(ch))
                    {
                        var expected = Matches[stack.Pop()];
                        if (expected != ch)
                        {
                            score += PartOneScores[ch];
                            break;
                        }
                    }
                }
            }
            return score.ToString();
        }

        private string[] ParseInput(string input)
        {
            return input.Split(Environment.NewLine, options: SSO.Value);
        }
        public string SolvePart2(string input)
        {
            string[] lines = ParseInput(input);
            List<long> lineScores = new();
            for (int i = 0; i < lines.Length; i++)
            {
                var stack = new Stack<char>();
                bool corrupt = false;
                foreach (var ch in lines[i])
                {
                    if (OpeningBraces.Contains(ch))
                    {
                        stack.Push(ch);
                    }
                    else if (ClosingBraces.Contains(ch))
                    {
                        var expected = Matches[stack.Pop()];
                        if (expected != ch)
                        {
                            // line is corrupt - do not score
                            corrupt = true;
                        }
                    }
                }

                if (!corrupt)
                {
                    var missingChars = stack.Select(s => Matches[s]).ToList();
                    lineScores.Add(missingChars
                        .Aggregate(0L, (seed, c) => (seed * 5L) + PartTwoScores[c]));
                }
                
            }
            lineScores.Sort();
            return lineScores[(lineScores.Count - 1) / 2].ToString();   
        }
    }
}