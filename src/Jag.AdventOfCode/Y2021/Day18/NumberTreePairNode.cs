using System;

namespace Jag.AdventOfCode.Y2021.Day18
{
    public class NumberTreePairNode : NumberTreeNode
    {
        public NumberTreePairNode()
        {
        }
        
        public NumberTreePairNode(NumberTreeNode left, NumberTreeNode right)
        {
            Left = left;
            Right = right;
            left.Parent = this;
            right.Parent = this;
        }
        
        public NumberTreePairNode(NumberTreePairNode parent, NumberTreeNode left, NumberTreeNode right)
            : this (left, right)
        {
            Parent = parent;
        }

        public NumberTreePairNode(long left, long right)
            : this(new NumberTreeNumberNode(left), new NumberTreeNumberNode(right))
        {
        }

        public NumberTreeNode Left { get; set; }
        public NumberTreeNode Right { get; set; }

        public NumberTreeNumberNode Explode()
        {
            var left = Left as NumberTreeNumberNode;
            var right = Right as NumberTreeNumberNode;

            var leftNeighbour = left.GetNeighbourNumberNodeToTheLeft();
            if (left != leftNeighbour && leftNeighbour != null)
            {
                leftNeighbour.Value += left.Value;
            }
            var rightNeighbour = right.GetNeighbourNumberNodeToTheRight();
            if (right != rightNeighbour && rightNeighbour != null)
            {
                rightNeighbour.Value += right.Value;
            }

            return new NumberTreeNumberNode(0);
        }

        public void Add(NumberTreeNode node)
        {
            node.Parent = this;
            if (Left == null) 
            {
                Left = node;
            }
            else if (Right == null) 
            {
                Right = node;
            }
            else 
            {
                throw new Exception("Left and right already filled");
            }
        }

        public void Replace(NumberTreeNode node, NumberTreeNode newNode)
        {
            if (Left == node)
            {
                Left = newNode;
                newNode.Parent = this;
            }
            else if (Right == node)
            {
                Right = newNode;
                newNode.Parent = this;
            }
            else
            {
                throw new Exception("node is not left or right");
            }    
        }

        public override long GetMagnitude()
        {
            var left = Left.GetMagnitude();
            var right = Right.GetMagnitude();

            return (left * 3) + (right * 2);
        }
    }
}