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

        public int MaxRow { get; private set; }

        public int MinRow { get; private set; }

        public int MaxCol { get; private set; }

        public int MinCol { get; private set; }

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
                    UpdateBounds(key);
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

        public object Clone()
        {
            return new SparseGrid<T>(new Dictionary<(int Row, int Col), T>(points));
        }

        private void UpdateBounds((int row, int col) key)
        {
            var (row, col) = key;
            if (row > MaxRow) MaxRow = row;
            if (row < MinRow) MinRow = row;
            if (col > MaxCol) MaxCol = col;
            if (col < MinCol) MinCol = col;
        }

    }
}
