using System;

namespace Jag.AdventOfCode.Y2021.Day19
{
    public record Beacon (int X, int Y, int Z)
    {
        public Beacon RotateAroundScanner(Func<int, int, int, (int X, int Y, int Z)> rotation)
        {
            var (x, y, z) = rotation(X, Y, Z);
            return new Beacon(x, y, z);
        }

        public Beacon MoveBeacon(int x, int y, int z)
        {
            return new Beacon(X + x, Y + y, Z + z);
        }
    }
}