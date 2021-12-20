using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Jag.AdventOfCode.Y2021.Day20
{
    public class SparseGrid
    {
        public SparseGrid()
            : this (new())
        {   
        }

        private SparseGrid( Dictionary<(int Row, int Col), bool> points)
        {
            this.points = points;
        }

        private readonly Dictionary<(int Row, int Col), bool> points = new();

        public IEnumerable<(int Row, int Col, bool Value)> EnumeratePoints()
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
        
        public bool this[(int row, int col) key] => this[key.row, key.col];

        public bool this[int row, int col]
        {
            get
            {
                var key = (row, col);
                if (points.TryGetValue(key, out bool value))
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
                if (!default(bool).Equals(value))
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

        private void UpdateBounds((int row, int col) key)
        {
            var (row, col) = key;
            if (row > MaxRow) MaxRow = row;
            if (row < MinRow) MinRow = row;
            if (col > MaxCol) MaxCol = col;
            if (col < MinCol) MinCol = col;
        }

        public bool HasInBoundsNeighbour((int row, int col) key) => UseBorderChar(key.row, key.col);

        private bool UseBorderChar(int row, int col)
        {
            bool use = row > MaxRow || row < MinRow
                || col > MaxCol || col < MinCol;
            //Console.WriteLine($"UseBorderChar {use} - {row}, {col}");
            return use;
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


        public SparseGrid EnhanceImage(bool[] imageAlgorithm, bool border)
        {
            var newGrid = new SparseGrid();

            for (int row = MinRow - 1; row <= MaxRow + 1; row++)
            {
                for (int col = MinCol - 1; col <= MaxCol + 1; col++)
                {
                    var lookupNumber = GetNumberForCell(row, col, border);
                    newGrid[row, col] = imageAlgorithm[lookupNumber];
                }
            }

            return newGrid;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int row = MinRow; row <= MaxRow; row++)
            {
                for (int col = MinCol; col <= MaxCol; col++)
                {
                    var c = this[row, col] ? '#' : '.';
                    sb.Append(c);
                }
                
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private int GetNumberForCell(int cellRow, int cellCol, bool border)
        {
            var colOffsets = new int[] { -1, 0, 1 };
            var rowOffsets = new int[] { -1, 0, 1 };
            var binaryStringBuilder = new StringBuilder();

            foreach (var rowOffset in rowOffsets)
            {
                foreach (int colOffset in colOffsets)
                {
                    var row = cellRow + rowOffset;
                    int col = cellCol + colOffset;
                    if (UseBorderChar(row, col))
                    {
                        binaryStringBuilder.Append(border ? '1' : '0');
                    }
                    else
                    {
                        binaryStringBuilder.Append(this[row, col] ? '1' : '0');
                    }
                }
            }

            return Convert.ToInt32(binaryStringBuilder.ToString(), 2);
        }
    }
}
