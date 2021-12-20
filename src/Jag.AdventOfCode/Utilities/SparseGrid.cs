using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Jag.AdventOfCode.Utilities
{
    public class SparseGrid
        : ICloneable
    {
        public SparseGrid()
        {   
        }

        private SparseGrid(HashSet<(int Row, int Col)> points)
        {
            this.points = points;
        }

        private readonly HashSet<(int Row, int Col)> points = new();

        public IEnumerable<(int Row, int Col)> EnumeratePoints()
        {
            foreach (var point in points)
            {
                yield return (point.Row, point.Col);
            }
        }

        public int MaxRow { get; private set; }

        public int MinRow { get; private set; }

        public int MaxCol { get; private set; }

        public int MinCol { get; private set; }
        
        public void Add((int row, int col) point) => Add(point.row, point.col);

        public void Add(int row, int col)
        {
            var point = (row, col);
            if (points.Add(point))
            {
                UpdateBounds(point);
            }
        }

        public void Remove((int row, int col) point) => Remove(point.row, point.col);

        public void Remove(int row, int col)
        {
            var point = (row, col);
            if (points.Remove(point))
            {
                RecalculateBounds();
            }
        }

        public bool HasNeighbours((int row, int col) point) => HasNeighbours(point.row, point.col);

        public bool HasNeighbours(int row, int col)
        {
            var colOffsets = new int[] { -1, 0, 1 };
            var rowOffsets = new int[] { -1, 0, 1 };

            foreach (var rowOffset in rowOffsets)
            {
                foreach (int colOffset in colOffsets)
                {
                    var neighbourKey = (row + rowOffset, col + colOffset);
                    if (points.Contains(neighbourKey))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void UpdateBounds((int row, int col) point)
        {
            var (row, col) = point;
            if (row > MaxRow) MaxRow = row;
            if (row < MinRow) MinRow = row;
            if (col > MaxCol) MaxCol = col;
            if (col < MinCol) MinCol = col;
        }

        private void RecalculateBounds()
        {
            foreach (var point in points)
            {
                UpdateBounds(point);
            }
        }

        public object Clone()
        {
            return new SparseGrid(new HashSet<(int Row, int Col)>(points));
        }
    }
}
