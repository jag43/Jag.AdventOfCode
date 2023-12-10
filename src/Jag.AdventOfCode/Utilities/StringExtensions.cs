namespace Jag.AdventOfCode.Utilities;

public static class StringExtensions 
{
    public static string SafeSubstring(this string orig, int start, int length)
    {
        if (start >= orig.Length) return string.Empty; // could return orig or null
        return orig.Substring(start, orig.Length >= start + length ? length : orig.Length - start);
    }
}