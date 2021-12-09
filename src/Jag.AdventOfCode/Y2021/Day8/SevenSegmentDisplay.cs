using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day8
{
    public class SevenSegmentDisplay
    {
        public const string Zero = "abcefg";
        public const string One = "cf";
        public const string Two = "acdeg";
        public const string Three = "acdfg";
        public const string Four = "bcdf";
        public const string Five = "abdfg";
        public const string Six = "abdefg";
        public const string Seven = "acf";
        public const string Eight = "abcdefg";
        public const string Nine = "abcdfg";

        public static Dictionary<char, int> BuildSegmentFrequency(IEnumerable<string> numbers)
        {
            var segmentFrequency = new Dictionary<char, int>();
            foreach (var ssd in SevenSegmentDisplay.Segements)
            {
                segmentFrequency.Add(ssd, numbers.Count(n => n.Contains(ssd)));
            }
            return segmentFrequency;
        }

        public static readonly IReadOnlyList<char> Segements = Eight.ToList().AsReadOnly();

        public static readonly IReadOnlyList<string> Numbers = new List<string>
        {
            Zero,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine
        }.AsReadOnly();

        public static int ConvertSegmentsToInt(params string[] segments)
        {
            var chars = new List<char>();
            foreach (var segment in segments)
            {
                chars.Add(SegmentToChar(segment));
            }
            return int.Parse(new string(chars.ToArray()));
        }

        private static char SegmentToChar(string segment)
        {
            switch(segment)
            {
                case Zero: return '0';
                case One: return '1';
                case Two: return '2';
                case Three: return '3';
                case Four: return '4';
                case Five: return '5';
                case Six: return '6';
                case Seven: return '7';
                case Eight: return '8';
                case Nine: return '9';
                default: throw new Exception();
            }
        }
    } 
}