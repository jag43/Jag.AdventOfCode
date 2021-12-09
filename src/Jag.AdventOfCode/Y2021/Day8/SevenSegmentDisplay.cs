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
    } 
}