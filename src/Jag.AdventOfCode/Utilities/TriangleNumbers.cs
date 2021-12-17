using System;

namespace Jag.AdventOfCode.Utilities
{
    public class TriangleNumbers
    {
        public static double TriangleNumber(int n) => (n * (n + 1)) / 2;

        public static int ReverseTriangleNumber(int triangleNumber) 
        {
            return (int)Math.Round((-1 + Math.Sqrt(1 + (4 * 2 * triangleNumber))) / 2);
        }
    }
}