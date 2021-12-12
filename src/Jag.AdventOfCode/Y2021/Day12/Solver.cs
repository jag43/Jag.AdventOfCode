using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day12
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 12;

        public string SolvePart1(string input)
        {
            var nodes = ParseInput(input);

            var allPaths = new List<List<Cave>>();
            FindAllPathsPart1(nodes, allPaths, new List<Cave>(), "start");

            return allPaths.Count.ToString();
        }

        public string SolvePart2(string input)
        {
            var nodes = ParseInput(input);

            var allPaths = new List<List<Cave>>();
            FindAllPathsPart2(nodes, allPaths, new List<Cave>(), "start");

            return allPaths.Count.ToString();
        }

        private Dictionary<string, Cave> ParseInput(string input)
        {
            var nodes = new Dictionary<string, Cave>();

            foreach (var line in input.Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries))
            {
                var lineNodes = line.Split("-", options: StringSplitOptions.RemoveEmptyEntries);
                AddNodes(nodes, lineNodes[0], lineNodes[1]);
            }

            return nodes;
        }

        private void AddNodes(Dictionary<string, Cave> nodes, string name, string linkedName)
        {
            Cave cave;
            if (nodes.ContainsKey(name))
            {
                cave = nodes[name];
            }
            else
            {
                cave = new Cave(name);
                nodes.Add(name, cave);
            }

            Cave linkedCave;
            if (nodes.ContainsKey(linkedName))
            {
                linkedCave = nodes[linkedName];
            }
            else 
            {
                linkedCave = new Cave(linkedName);
                nodes.Add(linkedName, linkedCave);
            }

            cave.Connections.Add(linkedName, linkedCave);
            linkedCave.Connections.Add(name, cave);
        }
    
        private void FindAllPathsPart1(Dictionary<string, Cave> nodes, List<List<Cave>> paths, List<Cave> currentPath, string currentNodeName)
        {
            var currentNode = nodes[currentNodeName];

            foreach (var linkedNode in currentNode.Connections.Values)
            {
                var path = currentPath.ToList();
                if (linkedNode.IsEnd)
                {
                    // finished path
                    path.Add(linkedNode);
                    paths.Add(path);
                    continue;
                }
                else if (linkedNode.IsStart)
                {
                    // don't go back to start
                    continue;
                }
                else if (!linkedNode.IsBig && path.Any(c => c.Name == linkedNode.Name))
                {
                    // can't visit a small cave twice
                    continue;
                }
                else
                {
                    path.Add(linkedNode);
                    FindAllPathsPart1(nodes, paths, path, linkedNode.Name);
                }
            }
        }
    
        private void FindAllPathsPart2(Dictionary<string, Cave> nodes, List<List<Cave>> paths, List<Cave> currentPath, string currentNodeName)
        {
            var currentNode = nodes[currentNodeName];

            bool visitedASmallCaveTwice = currentPath.Where(c => !c.IsBig).GroupBy(c => c.Name).Any(group => group.Count() > 1);

            foreach (var linkedNode in currentNode.Connections.Values)
            {
                var path = currentPath.ToList();
                if (linkedNode.IsEnd)
                {
                    // finished path
                    path.Add(linkedNode);
                    paths.Add(path);
                    continue;
                }
                else if (linkedNode.IsStart)
                {
                    // don't go back to start
                    continue;
                }
                else if (visitedASmallCaveTwice && !linkedNode.IsBig && path.Any(c => c.Name == linkedNode.Name))
                {
                    // can only visit a single small cave twice
                    continue;
                }
                else
                {
                    path.Add(linkedNode);
                    FindAllPathsPart2(nodes, paths, path, linkedNode.Name);
                }
            }
        }
    }
}