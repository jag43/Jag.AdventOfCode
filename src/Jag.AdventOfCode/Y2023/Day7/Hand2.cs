using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Jag.AdventOfCode.Y2023.Day7;

[DebuggerDisplay("{DebuggerDisplay}")]
public record Hand2 : IComparable<Hand2>
{
    private string DebuggerDisplay => $"{HandType} {Card1.ToLabel()} {Card2.ToLabel()} {Card3.ToLabel()} {Card4.ToLabel()} {Card5.ToLabel()}";
    public Hand2(params Card2[] cards)
    {
        if (cards.Length != 5)
        {
            throw new ArgumentException("Hand must contain exactly 5 cards", nameof(cards));
        }
        Card1 = cards[0];
        Card2 = cards[1];
        Card3 = cards[2];
        Card4 = cards[3];
        Card5 = cards[4];

        HandType = CalculateHandType();
    }

    public Hand2(Card2 card1, Card2 card2, Card2 card3, Card2 card4, Card2 card5)
    {
        Card1 = card1;
        Card2 = card2;
        Card3 = card3;
        Card4 = card4;
        Card5 = card5;

        HandType = CalculateHandType();
    }

    public Card2 Card1 { get; }
    public Card2 Card2 { get; }
    public Card2 Card3 { get; }
    public Card2 Card4 { get; }
    public Card2 Card5 { get; }
    public HandType HandType { get; }

    public override int GetHashCode()
    {
        return HashCode.Combine(Card1, Card2, Card3, Card4, Card5);
    }

    public int CompareTo(Hand2 hand)
    {
        var handTypeComparison = hand.HandType.CompareTo(HandType);
        if (handTypeComparison != 0)
        {
            return handTypeComparison;
        }

        var card1Comparison = hand.Card1.CompareTo(Card1);
        if (card1Comparison != 0)
        {
            return card1Comparison;
        }

        var card2Comparison = hand.Card2.CompareTo(Card2);
        if (card2Comparison != 0)
        {
            return card2Comparison;
        }

        var card3Comparison = hand.Card3.CompareTo(Card3);
        if (card3Comparison != 0)
        {
            return card3Comparison;
        }

        var card4Comparison = hand.Card4.CompareTo(Card4);
        if (card4Comparison != 0)
        {
            return card4Comparison;
        }

        var card5Comparison = hand.Card5.CompareTo(Card5);
        return card5Comparison;
    }

    public static Hand2 Parse(string rawHand)
    {
        if (rawHand.Length != 5)
        {
            throw new ArgumentException("Hand must contain exactly 5 cards", nameof(rawHand));
        }
        return new Hand2(rawHand.Select(c => c.ToCard2()).ToArray());
    }

    public IEnumerable<Card2> Cards()
    {
        yield return Card1;
        yield return Card2;
        yield return Card3;
        yield return Card4;
        yield return Card5;
    }

    private HandType CalculateHandType()
    {
        var groups = Cards().GroupBy(c => c);
        var numberOfGroups = groups.Count();
        var containsJoker = Cards().Any(c => c == Card2.Joker);
        if (numberOfGroups == 1)
        {
            return HandType.FiveOfAKind;
        }
        if (numberOfGroups == 2)
        {
            int firstGroupCount = groups.First().Count();
            if (containsJoker)
            {
                return HandType.FiveOfAKind;
            }
            if (firstGroupCount == 1 || firstGroupCount == 4)
            {
                return HandType.FourOfAKind;
            }
            if (firstGroupCount == 2 || firstGroupCount == 3)
            {
                return HandType.FullHouse;
            }
        }
        if (numberOfGroups == 3)
        {
            if (groups.Any(g => g.Count() == 3))
            {
                if (containsJoker)
                {
                    return HandType.FourOfAKind;
                }
                return HandType.ThreeOfAKind;
            }
            if (containsJoker)
            {
                var jokerGroup = groups.First(g => g.Key == Card2.Joker);
                if (jokerGroup.Count() == 3 || jokerGroup.Count() == 1)
                {
                    return HandType.FullHouse;
                }
                if (jokerGroup.Count() == 2)
                {
                    return HandType.FourOfAKind;
                }
            }
            return HandType.TwoPair;
        }

        if (numberOfGroups == 4)
        {
            if (containsJoker)
            {
                return HandType.ThreeOfAKind;
            }
            return HandType.OnePair;
        }

        if (containsJoker)
        {
            return HandType.OnePair;
        }
        return HandType.HighCard;
    }
}
