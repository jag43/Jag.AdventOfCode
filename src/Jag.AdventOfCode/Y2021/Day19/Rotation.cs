using System;
using System.Collections.Generic;

namespace Jag.AdventOfCode.Y2021.Day19
{
    public class Rotation
    {
        public IEnumerable<Func<int, int, int, (int X, int Y, int Z)>> Rotations { get; } = new List<Func<int, int, int, (int X, int Y, int Z)>>
        {
            (x, y, z) => ( x,  y,  z),
            (x, y, z) => ( x, -y, -z),
            (x, y, z) => ( x, -z,  y),
            (x, y, z) => ( x, z,  -y),
        };
    }   
}