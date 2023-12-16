using System;
using System.Collections;

namespace Jag.AdventOfCode.Y2023.Day7;

public enum Card2
{
    Ace = 14,
    King = 13,
    Queen = 12,
    Ten = 10,
    Nine = 9,
    Eight = 8,
    Seven = 7,
    Six = 6,
    Five = 5,
    Four = 4,
    Three = 3,
    Two = 2,
    Joker = 1
}

public static class Card2Extensions
{
    public static string ToLabel(this Card2 card)
    {
        return card switch
        {
            Card2.Ace => "A",
            Card2.King => "K",
            Card2.Queen => "Q",
            Card2.Joker => "J",
            Card2.Ten => "T",
            Card2.Nine => "9",
            Card2.Eight => "8",
            Card2.Seven => "7",
            Card2.Six => "6",
            Card2.Five => "5",
            Card2.Four => "4",
            Card2.Three => "3",
            Card2.Two => "2",
            _ => throw new ArgumentException($"Invalid card: {card}", nameof(card))
        };
    }
    
    public static Card2 ToCard2(this char c)
    {
        return c switch
        {
            'A' => Card2.Ace,
            'K' => Card2.King,
            'Q' => Card2.Queen,
            'J' => Card2.Joker,
            'T' => Card2.Ten,
            '9' => Card2.Nine,
            '8' => Card2.Eight,
            '7' => Card2.Seven,
            '6' => Card2.Six,
            '5' => Card2.Five,
            '4' => Card2.Four,
            '3' => Card2.Three,
            '2' => Card2.Two,
            _ => throw new ArgumentException($"Invalid card: {c}", nameof(c))
        };
    }
}