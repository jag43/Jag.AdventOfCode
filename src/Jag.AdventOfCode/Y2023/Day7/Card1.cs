using System;

namespace Jag.AdventOfCode.Y2023.Day7;

public enum Card1
{
    Ace = 14,
    King = 13,
    Queen = 12,
    Jack = 11,
    Ten = 10,
    Nine = 9,
    Eight = 8,
    Seven = 7,
    Six = 6,
    Five = 5,
    Four = 4,
    Three = 3,
    Two = 2
}

public static class CardExtensions
{
    public static string ToLabel(this Card1 card)
    {
        return card switch
        {
            Card1.Ace => "A",
            Card1.King => "K",
            Card1.Queen => "Q",
            Card1.Jack => "J",
            Card1.Ten => "T",
            Card1.Nine => "9",
            Card1.Eight => "8",
            Card1.Seven => "7",
            Card1.Six => "6",
            Card1.Five => "5",
            Card1.Four => "4",
            Card1.Three => "3",
            Card1.Two => "2",
            _ => throw new ArgumentException($"Invalid card: {card}", nameof(card))
        };
    }
    
    public static Card1 ToCard1(this char c)
    {
        return c switch
        {
            'A' => Card1.Ace,
            'K' => Card1.King,
            'Q' => Card1.Queen,
            'J' => Card1.Jack,
            'T' => Card1.Ten,
            '9' => Card1.Nine,
            '8' => Card1.Eight,
            '7' => Card1.Seven,
            '6' => Card1.Six,
            '5' => Card1.Five,
            '4' => Card1.Four,
            '3' => Card1.Three,
            '2' => Card1.Two,
            _ => throw new ArgumentException($"Invalid card: {c}", nameof(c))
        };
    }
}