using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day22
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 22;

        public string SolvePart1(string input)
        {
            var grid = new HashSet<(int, int, int)>();
            foreach (var line in ParseInput(input).WhereXYZInGridOf(50))
            {
                for (int x = line.X.Min; x <= line.X.Max; x++)
                {
                    if (x > 50) 
                    {
                        return "";
                    }
                    for (int y = line.Y.Min; y <= line.Y.Max; y++)
                    {
                        if (y > 50) 
                        {
                            return "";
                        }
                        for (int z = line.Z.Min; z <= line.Z.Max; z++)
                        {
                            if (z > 50) 
                            {
                                return "";
                            }
                            if (line.OnOrOff)
                            {
                                grid.Add((x, y, z));
                            }
                            else 
                            {
                                grid.Remove((x, y, z));
                            }
                        }
                    }
                }
            }
            return grid.Count().ToString();
        }

        public string SolvePart2(string input)
        {
            var grid = new HashSet<(int, int, int)>();
            foreach (var line in ParseInput(input))
            {
                for (int x = line.X.Min; x <= line.X.Max; x++)
                {
                    for (int y = line.Y.Min; y <= line.Y.Max; y++)
                    {
                        for (int z = line.Z.Min; z <= line.Z.Max; z++)
                        {
                            if (line.OnOrOff)
                            {
                                grid.Add((x, y, z));
                            }
                            else 
                            {
                                grid.Remove((x, y, z));
                            }
                        }
                    }
                }
            }
            return grid.Count().ToString();
        }

        public (bool OnOrOff, (int Min, int Max) X, (int Min, int Max) Y, (int Min, int Max) Z)[] ParseInput(
            string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            return lines.Select(line => 
            {
                var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                return (OnOrOff: ParseOnOff(split[0]), Line: split[1]);
            })
            .Select(line =>
            {
                var numbers = line.Line.Split(",")
                .Select(l => 
                {
                    var numbers = l.Substring(2).Split("..");
                    var pair = (int.Parse(numbers[0]), int.Parse(numbers[1]));
                    return pair;
                })
                .ToArray();
                return (line.OnOrOff, numbers[0], numbers[1], numbers[2]);
            })
            .ToArray();
        }

        private bool ParseOnOff(string onOrOff)
        {
            return onOrOff switch
            {
                "on" => true,
                "off" => false,
                _ => throw new Exception("Not on or off")
            };
        }
    }

    public static class SolverExtensions
    {
        public static IEnumerable<(bool OnOrOff, (int Min, int Max) X, (int Min, int Max) Y, (int Min, int Max) Z)> WhereXYZInGridOf(
            this (bool OnOrOff, (int Min, int Max) X, (int Min, int Max) Y, (int Min, int Max) Z)[] lines,
            int gridSize)
        {
            return lines.Where(line => line.X.Min >= -50 && line.X.Max <= 50
                && line.Y.Min >= -50 && line.Y.Max <= 50
                && line.Z.Min >= -50 && line.Z.Max <= 50);
        }
    }
}
