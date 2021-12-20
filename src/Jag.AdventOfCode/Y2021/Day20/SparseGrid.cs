using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Jag.AdventOfCode.Utilities;

namespace Jag.AdventOfCode.Y2021.Day20
{
    public class SparseGrid : SparseGrid<bool>
    {
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

        private bool UseBorderChar(int row, int col)
        {
            bool use = row > MaxRow || row < MinRow
                || col > MaxCol || col < MinCol;
            return use;
        }

    }
}
