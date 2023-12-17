using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day9
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 9;

        private const int Ridge = int.MaxValue;

        public string SolvePart1(string input)
        {
            var map = ParseInput(input);
            var lowestPoints = GetLowestPoints(map);

            return lowestPoints.Sum(point => point.value + 1).ToString();
        }

        public string SolvePart2(string input)
        {
            var map = ParseInput(input);
            var nines = Get9s(map);

            var basinMap = Enumerable.Repeat(0, map.Count)
                .Select(_ => new int?[map[0].Length])
                .ToArray();
            
            foreach (var nine in nines)
            {
                basinMap[nine.i][nine.k] = Ridge;
            }

            int basin = 0;

            for (int i = 0; i < map.Count; i++)
            {
                for (int k = 0; k < map[i].Length; k++)
                {
                    if (!basinMap[i][k].HasValue)
                    {
                        var adjacentPositions = GetAdjacentPositions(i, k, map[0].Length, map.Count);
                        var adjacentBasin = adjacentPositions.Select(adjPos => basinMap[adjPos.i][adjPos.k])
                            .FirstOrDefault(adjBasin => adjBasin.HasValue && adjBasin != int.MaxValue);
                        if (adjacentBasin.HasValue)
                        {
                            basinMap[i][k] = adjacentBasin.Value;
                        }
                        else
                        {
                            FillInNewBasin(basin, i, k, basinMap);
                            basin++;
                        }
                    }
                }
            }

            var basins = basinMap.SelectMany(m => m)
                .Where(i => i != int.MaxValue)
                .GroupBy(i => i)
                .OrderByDescending(group => group.Count());
            var top3 = basins.Take(3);
            var answer = top3.Aggregate(1, (seed, group) => seed * group.Count());
            return answer.ToString();
        }

        private List<int[]> ParseInput(string input)
        {
            var list = new List<int[]>();
            var lines = input.Split(Environment.NewLine, options : StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                list.Add(line.Select(s => int.Parse(s.ToString())).ToArray());
            }

            return list;
        }

        private List<(int i, int k, int value)> GetLowestPoints(List<int[]> map)
        {
            var lowestPoints = new List<(int i, int k, int value)>();
            for (int i = 0; i < map.Count; i++)
            {
                for (int k = 0; k < map[i].Length; k++)
                {
                    var adjacentPositions = GetAdjacentPositions(i, k, map[0].Length, map.Count);
                    var thisPositionValue = map[i][k];
                    if (adjacentPositions.All(adjacentPosition => map[adjacentPosition.i][adjacentPosition.k] > thisPositionValue))
                    {
                        lowestPoints.Add((i, k, map[i][k]));
                    }
                }
            }
            return lowestPoints;
        }

        private IEnumerable<(int i, int k)> GetAdjacentPositions(int i, int k, int xLength, int yLength)
        {
            var firstRow = i == 0;
            var firstColumn = k == 0;
            var lastRow = i == yLength - 1;
            var lastColumn = k == xLength - 1;
            if (!firstRow)
            {
                yield return (i - 1, k);
            }
            if (!firstColumn)
            {
                yield return (i, k - 1);
            }
            if (!lastRow)
            {
                yield return (i + 1, k);
            }
            if (!lastColumn)
            {
                yield return (i, k + 1);
            }
        }
        
        private IEnumerable<(int i, int k)> Get9s(List<int[]> map)
        {
           for (int i = 0; i < map.Count; i++)
                for (int k = 0; k < map[i].Length; k++)
                    if(map[i][k] == 9)
                        yield return (i, k);
        }

        private void FillInNewBasin(int basin, int i, int k, int?[][] basinMap)
        {
            basinMap[i][k] = basin;
            var basinPositions = new HashSet<(int i, int k)>();
            var basinRidges = new HashSet<(int i, int k)>();
            var positionsToCheck = new HashSet<(int i, int k)>() { (i, k) };
            while (positionsToCheck.Count > 0)
            {
                var thisPosition = positionsToCheck.First();
                var (thisI, thisK) = thisPosition;
                var thisValue = basinMap[thisI][thisK];
                var adjacentNonRidges = GetAdjacentPositions(thisI, thisK, basinMap[0].Length, basinMap.Length)
                    .Where(adj => basinMap[adj.i][adj.k] != int.MaxValue);
                foreach (var adj in adjacentNonRidges)
                {
                    if (!positionsToCheck.Contains(adj) && !basinRidges.Contains(adj) && ! basinPositions.Contains(adj))
                    {
                        positionsToCheck.Add(adj);
                    }
                }
                
                if (thisValue == int.MaxValue)
                {
                    basinRidges.Add(thisPosition);
                }
                else if (!thisValue.HasValue)
                {
                    basinPositions.Add(thisPosition);
                    basinMap[thisI][thisK] = basin;
                }
                positionsToCheck.Remove(thisPosition);
            }
        }

    }
}