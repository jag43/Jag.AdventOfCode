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

            var paths = FindAllPaths(nodes);

            return paths.Count.ToString();
        }

        private List<List<string>> FindAllPaths(Dictionary<string, Cave> nodes)
        {
            var allPaths = new List<List<string>>();
   
            FindAllPaths(nodes, allPaths, new List<string>(), "start");

            return allPaths;
        }

        private void FindAllPaths(Dictionary<string, Cave> nodes, List<List<string>> paths, List<string> currentPath, string currentNodeName)
        {
            var currentNode = nodes[currentNodeName];

            foreach (var linkedNode in currentNode.Connections.Values)
            {
                var path = currentPath.ToList();
                if (linkedNode.IsEnd)
                {
                    // finished path
                    path.Add(linkedNode.Name);
                    paths.Add(path);
                    continue;
                }
                else if (linkedNode.IsStart)
                {
                    // don't go back to start
                    continue;
                }
                else if (!linkedNode.IsBig && path.Contains(linkedNode.Name))
                {
                    // can't visit a small cave twice
                    continue;
                }
                else
                {
                    path.Add(linkedNode.Name);
                    FindAllPaths(nodes, paths, path, linkedNode.Name);
                }
            }
        }

        public string SolvePart2(string input)
        {
            var nodes = ParseInput(input);

            throw new NotImplementedException();
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
    }
}