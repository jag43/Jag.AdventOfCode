using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day18
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 18;

        public string SolvePart1(string input)
        {
            var nodes = ParseInput(input);
            var first = nodes[0];
            var result = nodes.Skip(1).Aggregate(first, (l, r) => l + r);
            return result.GetMagnitude().ToString();
        }

        public string SolvePart2(string input)
        {
            throw new NotImplementedException();
        }

        public NumberTreeNode[] ParseInput(string input)
        {
            var lines = input.Split(Environment.NewLine, options: StringSplitOptions.RemoveEmptyEntries);

            var nodes = lines.Select(line => ParseLine(line)).ToArray();

            return nodes;
        }

        private static IReadOnlyCollection<char> Numbers = "0123456789".ToList().AsReadOnly();

        private NumberTreeNode ParseLine(string line)
        {
            var stack = new Stack<NumberTreePairNode>();
            NumberTreeNode first = null;
            foreach (var c in line)
            {
                if (c == '[')
                {
                    var node = new NumberTreePairNode();
                    if (first == null) { first = node; }
                    stack.Push(node);
                }
                else if (Numbers.Contains(c))
                {
                    var pair = stack.Peek();
                    var numberNode = new NumberTreeNumberNode(long.Parse(c.ToString()));
                    pair.Add(numberNode);
                }
                else if (c == ']')
                {
                    var pair = stack.Pop();
                    if (pair.Right == null || pair.Left == null)
                    {
                        throw new Exception("Pair not finished");
                    }
                    if (stack.TryPeek(out var peek))
                    {
                        peek.Add(pair);
                    }
                }
            }
            if (stack.Any())
            {
                throw new Exception("Stack is not empty");
            }
            return first;
        }
    }
}