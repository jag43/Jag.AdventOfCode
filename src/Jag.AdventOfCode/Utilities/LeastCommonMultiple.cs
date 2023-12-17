namespace Jag.AdventOfCode.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;

public class LeastCommonMultiple
{
    public static long Calculate(IEnumerable<long> numbers)
    {
        return numbers.Aggregate(Calculate);
    }
    
    public static long Calculate(long a, long b)
    {
        return Math.Abs(a * b) / GreatedCommonDivisor(a, b);
    }

    public static long GreatedCommonDivisor(long a, long b)
    {
        return b == 0 ? a : GreatedCommonDivisor(b, a % b);
    }
}
