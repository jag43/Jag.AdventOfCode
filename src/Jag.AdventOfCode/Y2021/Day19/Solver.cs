using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day19
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 19;

        public string SolvePart1(string input)
        {
            var scanners = ParseInput(input);

            List<(int, int)> scannerOverlaps = new();

            foreach (var scanner1 in scanners)
            {
                foreach (var scanner2 in scanners.Where(s => s.Number != scanner1.Number))
                {
                    if (scanner1.Overlaps(scanner2) != null)
                    {
                        scannerOverlaps.Add((scanner1.Number, scanner2.Number));
                    }
                }
            }

            return "";
        }

        public string SolvePart2(string input)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Scanner> ParseInput(string input)
        {
            string[] scanners = input.Split(Environment.NewLine + Environment.NewLine, options: SSO.Value);
            foreach (var scannerInput in scanners)
            {
                var scannerLines = scannerInput.Split(Environment.NewLine, options: SSO.Value);
                var scanner = new Scanner()
                {
                    Number =  int.Parse(scannerLines[0].Substring(12, 2).Trim())
                };

                var beacons = scannerLines.Skip(1).Select(line => 
                {
                    var xyz = line.Split(",").Select(s => int.Parse(s)).ToArray();
                    return new Beacon(xyz[0], xyz[1], xyz[2]);
                });

                foreach (var beacon in beacons)
                {
                    scanner.Beacons.Add(beacon);
                }

                yield return scanner;
            }
        }
    }
}