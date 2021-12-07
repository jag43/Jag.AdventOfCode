using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day3
{
    public class Solver : ISolver
    {
        public int Year { get; } = 2021;

        public int Day { get; } = 3;

        public string SolvePart1(string input)
        {
            return string.Empty;
        }

        public string SolvePart2(string input)
        {
            var lines = input.Split(Environment.NewLine).Select(b => b.ToCharArray());

            var oxygenString = new string(GetOxygenString(lines));
            var co2scrubString = new string(GetCo2String(lines));

            var oxygen = Convert.ToUInt64(oxygenString);
            var co2scrub = Convert.ToUInt64(co2scrubString);

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(new{oxygenString, oxygen, co2scrub, co2scrubString}));

            return (oxygen * co2scrub).ToString();
        }

        private static char[] GetOxygenString(IEnumerable<char[]> input)
        {
            var lines = input.ToList();
            var length = lines[0].Length;
            for (int i = 0; i < length; i++)
            {
                var (num0s, num1s) = GetNum0And1s(lines, i);

                var ch = num1s == num0s ? '1'
                    : num1s > num0s ? '1'
                    : '0';

                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(new{i, num1s,num0s,ch}));
                lines.RemoveAll(l => l[i] != ch);

                if (lines.Count == 1)
                {
                    var result = lines.Single();
                    return result;
                }
            }

            throw new Exception();
        }

        private static char[] GetCo2String(IEnumerable<char[]> input)
        {
            var lines = input.ToList();
            var length = lines[0].Length;
            for (int i = 0; i < length; i++)
            {
                var (num0s, num1s) = GetNum0And1s(lines, i);

                var ch = num1s == num0s ? '0'
                    : num1s > num0s ? '0'
                    : '1';

                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(new{i, num1s,num0s,ch}));
                lines.RemoveAll(l => l[i] != ch);

                if (lines.Count == 1)
                {
                    var result = lines.Single();
                    return result;
                }
            }

            throw new Exception();
        }

        private static (int num0s, int num1s) GetNum0And1s(List<char[]> lines, int i)
        {
            int num0s = 0;
            int num1s = 0;

            foreach (var line in lines)
            {
                var current = line[i];
                if (current == '0')
                {
                    num0s++;
                }
                else if (current == '1')
                {
                    num1s++;
                }
                else
                {
                    throw new Exception();
                }
            }
            return (num0s, num1s);
        }
    }
}
