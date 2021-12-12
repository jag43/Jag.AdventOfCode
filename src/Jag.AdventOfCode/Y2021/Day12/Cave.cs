using System;
using System.Collections.Generic;

namespace Jag.AdventOfCode.Y2021.Day12
{
    public class Cave
        {
        public Cave(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public bool IsStart => string.Equals(Name, "start", StringComparison.InvariantCultureIgnoreCase);

        public bool IsEnd => string.Equals(Name, "end", StringComparison.InvariantCultureIgnoreCase);

        public bool IsBig => !IsStart && !IsEnd && string.Equals(Name, Name.ToUpperInvariant(), StringComparison.InvariantCulture);

        public Dictionary<string, Cave> Connections { get; } = new Dictionary<string, Cave>();
    }
}