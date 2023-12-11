using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Utilities;

public static class CharExtensions
{
    private static readonly IReadOnlyList<char> digits = new List<char> {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }.AsReadOnly();

    public static bool IsDigit(this char c)
    {
        return digits.Contains(c);
    }
}