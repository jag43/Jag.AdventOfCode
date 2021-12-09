using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jag.AdventOfCode;
using Newtonsoft.Json;

namespace Jag.AdventOfCode.Y2021.Day6
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 6;

        public string SolvePart1(string input)
        {
            var fish = ParseInput(input);
            return GetNumberOfFishAfterDays(fish, 80);
        }

        public string SolvePart2(string input)
        {
            var fish = ParseInput(input);
            return GetNumberOfFishAfterDays(fish, 256);
        }

        private int[] ParseInput(string input)
        {
            return input
                .Split(",")
                .Select(s => int.Parse(s))
                .ToArray();
        }

        protected string GetNumberOfFishAfterDays(int[] fish, int days)
        {
            var buckets = Enumerable.Repeat(0L, 9).ToArray();
            foreach (var f in fish)
            {
                buckets[f]++;
            }

            for (int i = 0; i < days; i++)
            {
                var prevBuckets = buckets;
                buckets = Enumerable.Repeat(0L, 9).ToArray();
                for (int k = 0; k < buckets.Length; k++)
                {
                    if (k == 0)
                    {
                        buckets[8] = prevBuckets[0];
                        buckets[6] = prevBuckets[0];
                    }
                    else 
                    {
                        buckets[k-1] += prevBuckets[k];
                    }
                }
            }

            return buckets.Sum().ToString();
        }
    }
}
