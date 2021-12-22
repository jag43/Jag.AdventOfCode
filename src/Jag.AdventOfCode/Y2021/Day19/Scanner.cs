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
                        var movedScanner = rotatedScanner.AlignPositionWithBeacon(otherBeacon, thisBeacon);
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

        public Scanner AlignPositionWithBeacon(Beacon otherBeacon, Beacon thisBeacon)
        {
            return MoveScanner(otherBeacon.X - thisBeacon.X,
                otherBeacon.Y - thisBeacon.Y,
                otherBeacon.Z - thisBeacon.Z);
        }

        private Scanner MoveScanner(int x, int y, int z)
        {
            var scanner = new Scanner() { Number = Number };

            foreach (var beacon in Beacons)
            {
                scanner.Beacons.Add(beacon.MoveBeacon(x, y, z));
            }

            return scanner;
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