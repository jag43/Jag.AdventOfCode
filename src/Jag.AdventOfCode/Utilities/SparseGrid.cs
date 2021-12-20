using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Jag.AdventOfCode.Utilities
{
    public class SparseGrid<T>
        : ICloneable
    {
        public SparseGrid()
        {   
        }

        private SparseGrid(Dictionary<(int Row, int Col), T> points)
        {
            this.points = points;
        }

        private readonly Dictionary<(int Row, int Col), T> points = new();

        public IEnumerable<(int Row, int Col, T Value)> EnumeratePoints()
        {
            foreach (var key in points.Keys)
            {
                yield return (key.Row, key.Col, points[key]);
            }
        }

        public int MaxRow => points.Keys.Any() ? points.Keys.Max(key => key.Row) : 0;

        public int MinRow => points.Keys.Any() ? points.Keys.Min(key => key.Row) : 0;

        public int MaxCol => points.Keys.Any() ? points.Keys.Max(key => key.Col) : 0;

        public int MinCol => points.Keys.Any() ? points.Keys.Min(key => key.Col) : 0;
        
        public T this[(int row, int col) key] => this[key.row, key.col];

        public T this[int row, int col] 
        {
            get
            {
                if (points.TryGetValue((row, col), out T value))
                {
                    return value;
                }
                else
                {
                    return default;
                }
            }
            set
            {
                var key = (row, col);
                if (!default(T).Equals(value))
                {
                    points[key] = value;
                }
                else if (points.ContainsKey(key))
                {
                    points.Remove(key);
                }
            }
        }

        public bool HasNeighbours((int row, int col) key) => HasNeighbours(key.row, key.col);

        public bool HasNeighbours(int row, int col)
        {
            var colOffsets = new int[] { -1, 0, 1 };
            var rowOffsets = new int[] { -1, 0, 1 };

            foreach (var rowOffset in rowOffsets)
            {
                foreach (int colOffset in colOffsets)
                {
                    var neighbourKey = (row + rowOffset, col + colOffset);
                    if (points.ContainsKey(neighbourKey))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public SparseGrid<T> Bound(int maxRowBounds, int minRowBounds, int maxColBounds, int minColBounds)
        {
            var inBoundPoints = new Dictionary<(int, int), T>();
            foreach (var point in points)
            {
                var key = point.Key;
                if (key.Row < maxRowBounds && key.Row > minRowBounds 
                    && key.Col < maxColBounds && key.Col > minColBounds)
                {
                    inBoundPoints.Add(point.Key, point.Value);
                }
            }

            return new(inBoundPoints);
        }

        public object Clone()
        {
            return new SparseGrid<T>(new Dictionary<(int Row, int Col), T>(points));
        }
    }
}
