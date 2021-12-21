using System;
using System.Collections.Generic;

namespace Jag.AdventOfCode.Y2021.Day19
{
    public static class Rotation
    {
        public static IEnumerable<Func<int, int, int, (int X, int Y, int Z)>> NinetyDegreeRotations { get; } = new List<Func<int, int, int, (int X, int Y, int Z)>>
        {
            // 4 times X to the right
            (x, y, z) => ( x,  y,  z),
            (x, y, z) => ( x,  z, -y),
            (x, y, z) => ( x, -y, -z),
            (x, y, z) => ( x, -z,  y),
            // 4 times -X to the right
            (x, y, z) => (-x,  z,  y),
            (x, y, z) => (-x,  y, -z),
            (x, y, z) => (-x, -z, -y),
            (x, y, z) => (-x, -y,  z),
            // 4 times Y to the right
            (x, y, z) => ( y,  z,  x),
            (x, y, z) => ( y,  x, -z),
            (x, y, z) => ( y, -z, -x),
            (x, y, z) => ( y, -x,  z),
            // 4 times -Y to the right
            (x, y, z) => (-y,  x,  z),
            (x, y, z) => (-y,  z, -x),
            (x, y, z) => (-y, -x, -z),
            (x, y, z) => (-y, -z,  x),
            // 4 times Z to the right
            (x, y, z) => ( z,  x,  y),
            (x, y, z) => ( z,  y, -x),
            (x, y, z) => ( z, -x, -y),
            (x, y, z) => ( z, -y,  x),
            // 4 times -Z to the right
            (x, y, z) => (-z,  y,  x),
            (x, y, z) => (-z,  x, -y),
            (x, y, z) => (-z, -y, -x),
            (x, y, z) => (-z, -x,  y),
        };
    }   
}