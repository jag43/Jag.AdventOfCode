using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day19
{
    public class Scanner
    {
        public int Number { get; set; }

        public (int X, int Y, int Z) Position { get; } = (0, 0, 0);

        public HashSet<Beacon> Beacons { get; } = new();

        public Func<int, int, int, (int X, int Y, int Z)> Overlaps(Scanner scanner)
        {
            foreach (var thisBeacon in this.Beacons)
            {
                foreach (var rotation in Rotation.NinetyDegreeRotations)
                {
                    var rotatedScanner = scanner.Rotate(rotation);
                    foreach(var otherBeacon in rotatedScanner.Beacons)
                    {
                        var movedScanner = rotatedScanner.AlignBeacons(otherBeacon, thisBeacon);
                        var matchingBeacons = this.Beacons.Join(
                            movedScanner.Beacons,
                            b => b,
                            b => b,
                            (l, r) => (l, r));
                        var matchingBeaconsCount = matchingBeacons.Count();
                        if (matchingBeaconsCount >= 12 
                            || matchingBeaconsCount == this.Beacons.Count
                            || matchingBeaconsCount == scanner.Beacons.Count)
                        {
                            return rotation;
                        }
                    }
                }
            }

            return null;
        }

        public Scanner AlignBeacons(Beacon otherBeacon, Beacon thisBeacon)
        {
            throw new NotImplementedException();
        }

        private Scanner Rotate(Func<int, int, int, (int X, int Y, int Z)> rotation)
        {
            var scanner = new Scanner();
            foreach (var beacon in Beacons)
            {
                scanner.Beacons.Add(beacon.RotateAroundScanner(rotation));
            }

            return scanner;
        }
    }
}