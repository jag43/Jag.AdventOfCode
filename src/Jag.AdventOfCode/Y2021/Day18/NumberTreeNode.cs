using System;
using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Y2021.Day18
{
    public abstract class NumberTreeNode
    {
        public NumberTreeNode()
        {
        }

        public NumberTreeNode(NumberTreePairNode parent)
        {
            Parent = parent;
        }

        public NumberTreePairNode Parent { get; set; }

        public bool IsLeft => Parent?.Left == this;

        public bool IsRight => Parent?.Right == this;

        public abstract long GetMagnitude();

        public bool IsTopLevel => Parent == null;

        public NumberTreeNumberNode GetLeftMostNode()
        {
            return GetXMostNode(pairNode => pairNode.Left.GetLeftMostNode());
        }

        public NumberTreeNumberNode GetRightMostNode()
        {
            return GetXMostNode(pairNode => pairNode.Right.GetRightMostNode());
        }

        public static NumberTreeNode operator + (NumberTreeNode left, NumberTreeNode right)
        {
            var result = new NumberTreePairNode(left, right);
            result.Reduce();
            return result;
        }

        public void Reduce()
        {
            var (node, action) = GetNextAction();
            while (action != ReduceAction.None)
            {
                if (action == ReduceAction.Explode)
                {
                    var newNode = node.Parent.Explode();
                    node.Parent.Parent.Replace(node.Parent, newNode);
                }
                else if (action == ReduceAction.Split)
                {
                    var newNode = node.Split();
                    node.Parent.Replace(node, newNode);
                }
                (node, action) = GetNextAction();
            }
        }

        public (NumberTreeNumberNode Node, ReduceAction Action) GetNextAction()
        {
            var nodes = GetNumbersFromLeftToRight();
            foreach (var numberNode in nodes)
            {
                if (numberNode?.Parent?.Parent?.Parent?.Parent != null
                    && numberNode.Parent.Left is NumberTreeNumberNode
                    && numberNode.Parent.Right is NumberTreeNumberNode)
                {
                    return (numberNode, ReduceAction.Explode);
                }
            }
            foreach (var numberNode in nodes)
            {
                if (numberNode.Value >= 10)
                {
                    return (numberNode, ReduceAction.Split);
                }
            }

            return (null, ReduceAction.None);
        }

        public IEnumerable<NumberTreeNumberNode> GetNumbersFromLeftToRight()
        {
            var leftMost = this.GetLeftMostNode();
            yield return leftMost;
            var next = leftMost.GetNeighbourNumberNodeToTheRight();
            while(next != null)
            {
                if (next == null) yield break;
                yield return next;
                next = next.GetNeighbourNumberNodeToTheRight();
            }
        }

        private NumberTreeNumberNode GetXMostNode(Func<NumberTreePairNode, NumberTreeNumberNode> getXNode)
        {
            if (this is NumberTreeNumberNode numberNode)
            {
                return numberNode;
            }
            else if (this is NumberTreePairNode pairNode)
            {
                return getXNode(pairNode);
            }

            throw new Exception($"NumberTreeNode type not implemented {this.GetType().FullName}");
        }
    }
}