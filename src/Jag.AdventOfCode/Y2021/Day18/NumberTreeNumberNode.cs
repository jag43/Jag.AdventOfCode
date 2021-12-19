using System;

namespace Jag.AdventOfCode.Y2021.Day18
{
    public class NumberTreeNumberNode
        : NumberTreeNode
    {
        public NumberTreeNumberNode(long value)
        {
            Value = value;
        }

        public NumberTreeNumberNode(NumberTreePairNode parent, long value)
            : base(parent)
        {
            Value = value;
        }

        public long Value { get; set; }

        public NumberTreeNumberNode GetNeighbourNumberNodeToTheLeft()
        {
            if (Parent == null) 
            {
                return null;
            }
            else if (IsLeft)
            {
                NumberTreePairNode next = this.Parent;
                while (true)
                {
                    if (next.IsTopLevel)
                    {
                        return null;
                    }
                    if (next.IsRight)
                    {
                        return next.Parent.Left.GetRightMostNode();
                    }
                    next = next.Parent;
                }
            }
            else if (IsRight)
            {
                return Parent.Left.GetRightMostNode();
            }

            throw new Exception($"NumberTreeNode type not implemented {this.GetType().FullName}");
        }

        public NumberTreeNumberNode GetNeighbourNumberNodeToTheRight()
        {
           if (Parent == null) 
            {
                return null;
            }
            else if (IsRight)
            {
                NumberTreePairNode next = this.Parent;
                while (true)
                {
                    if (next.IsTopLevel)
                    {
                        return null;
                    }
                    if (next.IsLeft)
                    {
                        return next.Parent.Right.GetLeftMostNode();
                    }
                    next = next.Parent;
                }
            }
            else if (IsLeft)
            {
                return Parent.Right.GetLeftMostNode();
            }

            throw new Exception($"NumberTreeNode type not implemented {this.GetType().FullName}");
        }

        public NumberTreePairNode Split()
        {
            var left = (int) Math.Floor(Value / 2M);
            var right = (int) Math.Ceiling(Value / 2M);

            return new NumberTreePairNode(left, right);
        }

        public override long GetMagnitude()
        {
            return Value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}