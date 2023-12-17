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
            NumberTreeNode first = nodes[0];
            var result = nodes.Skip(1).Aggregate(first, (l, r) => l + r);
            return result.GetMagnitude().ToString();
        }

        public string SolvePart2(string input)
        {
            long max = 0;
            NumberTreePairNode[] array1 = ParseInput(input);
            for (int i = 0; i < array1.Length; i++)
            {
                NumberTreePairNode node1 = array1[i];
                NumberTreePairNode[] array2 = ParseInput(input);
                for (int k = 0; k < array2.Length; k++)
                {
                    NumberTreePairNode node2 = array2[k];
                    if (i != k)
                    {
                        var mag = (node1 + node2).GetMagnitude();
                        if (mag > max) 
                        {
                            max = mag;
                        }
                    }
                }
            }

            return max.ToString();
        }

        public NumberTreePairNode[] ParseInput(string input)
        {
            var lines = input.Split(Environment.NewLine, options: SSO.Value);

            var nodes = lines.Select(line => ParseLine(line)).ToArray();

            return nodes;
        }

        private static IReadOnlyCollection<char> Numbers = "0123456789".ToList().AsReadOnly();

        private NumberTreePairNode ParseLine(string line)
        {
            var stack = new Stack<NumberTreePairNode>();
            NumberTreePairNode first = null;
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