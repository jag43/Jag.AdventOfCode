using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace Jag.AdventOfCode.Y2023.Day4
{
    public class Solver : ISolver
    {
        public int Year => 2023;

        public int Day => 4;

        public string SolvePart1(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(line => !string.IsNullOrWhiteSpace(line));
            var cards = lines.Select(Card.Parse);
            var sum = 0;
            foreach (var card in cards)
            {
                if (card.NumberOfWinningNumberMatches == 1 )
                { 
                    sum += 1;
                }
                else
                {
                    sum += (int)Math.Pow(2, card.NumberOfWinningNumberMatches - 1);
                }
            }
            return sum.ToString();
        }

        public string SolvePart2(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(line => !string.IsNullOrWhiteSpace(line));
            var cards = lines.Select(Card.Parse).ToList();
            for (int cardIndex = 0; cardIndex < cards.Count; cardIndex++)
            {
                var card = cards[cardIndex];
                if (card.NumberOfWinningNumberMatches > 0)
                {
                    for (int copy = 1; copy <= card.NumberOfWinningNumberMatches; copy++)
                    {
                        int indexToCopy = cardIndex + copy;
                        if (indexToCopy < cards.Count)
                        {
                            cards[indexToCopy].Copies += card.Copies;
                        }
                    }
                }
            }
            return cards.Sum(card => card.Copies).ToString();
        }
    }
}