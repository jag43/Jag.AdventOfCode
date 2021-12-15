using System;
using System.Collections.Generic;
using System.Linq;
using Jag.AdventOfCode.Extensions;

namespace Jag.AdventOfCode.Y2021.Day14
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 14;

        public string SolvePart1(string input)
        {
            return ApplyRules(input, 10);
        }

        public string SolvePart2(string input)
        {
            return ApplyRules(input, 40);
        }

        private string ApplyRules(string input, int times)
        {
            var (template, rules) = ParseInput(input);

            var buckets = new Dictionary<Pair, long>();
            foreach (var pair in template.ToCharArray().BreakDownIntoWindows(2).Select(c => new Pair(c[0], c[1])))
            {
                if (!buckets.ContainsKey(pair))
                {
                    buckets[pair] = 1;
                }
                else
                {
                    buckets[pair]++;
                }

            }
            for (int i = 0; i < times; i++)
            {
                var newBuckets = new Dictionary<Pair, long>();
                foreach (var bucket in buckets)
                {
                    var (newLeft, newRight) = rules[bucket.Key];
                    if (!newBuckets.ContainsKey(newLeft))
                    {
                        newBuckets[newLeft] = bucket.Value;
                    }
                    else
                    {
                        newBuckets[newLeft] += bucket.Value;
                    }
                    if (!newBuckets.ContainsKey(newRight))
                    {
                        newBuckets[newRight] = bucket.Value;
                    }
                    else
                    {
                        newBuckets[newRight] += bucket.Value;
                    }
                }
                buckets = newBuckets;
                var x = GetNumChars(template, buckets);
            }
            var numChars = GetNumChars(template, buckets);

            return (numChars.Max(g => g.Value) - numChars.Min(g => g.Value)).ToString();
        }

        private static Dictionary<char, long> GetNumChars(string template, Dictionary<Pair, long> buckets)
        {
            var numChars = "abcdefghijklmnopqrstuvwxyz".ToUpper().ToDictionary(c => c, c => 0L);
            foreach (var bucket in buckets)
            {
                numChars[bucket.Key.left] += bucket.Value;
                numChars[bucket.Key.right] += bucket.Value;
            }

            numChars = numChars.ToDictionary(c => c.Key, c => c.Value / 2);

            numChars[template.First()]++;
            numChars[template.Last()]++;

            numChars = numChars.Where(c => c.Value > 0).ToDictionary(c => c.Key, c => c.Value);
            return numChars;
        }

        public (string template, Dictionary<Pair, (Pair LeftPair, Pair RightPair)> rules) ParseInput(string input)
        {
            var lines = input.Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries);
            var template = lines[0];
            var pairs = lines.Skip(1)
                .Select(s => s.Split(" -> "))
                .ToDictionary(
                    arr => new Pair(arr[0][0], arr[0][1]),
                    arr => (new Pair(arr[0][0], arr[1][0]), new Pair(arr[1][0], arr[0][1])));

            return (template, pairs);
        }

        public record Pair (char left, char right);
    }
}