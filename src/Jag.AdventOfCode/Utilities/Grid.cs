using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jag.AdventOfCode.Utilities
{
    public static class Grid
    {
        public static IReadOnlyCollection<(int row, int col)> RelativeAdjacentNeighbours { get; } = new List<(int, int)>
        {
            (-1, 0), // down
            (0, -1), // left
            (0, 1), // up
            (1, 0), // down
        }.AsReadOnly();

        public static IReadOnlyCollection<(int row, int col)> RelativeDiagonalNeighbours { get; } = new List<(int, int)>
        {
            (-1, -1), // down left
            (-1, 1), // down right
            (1, 1), // up right
            (1, -1), // down right
        }.AsReadOnly();

        public static IReadOnlyCollection<(int row, int col)> RelativeNeighbours { get; } = RelativeDiagonalNeighbours.Concat(RelativeAdjacentNeighbours).ToList().AsReadOnly();

        public static IEnumerable<(int row, int col)> GetAdjacentNeighbours<T>(this IList<IList<T>> grid, (int row, int col) position) 
            => RelativeAdjacentNeighbours.GetNeighbours(grid, position);

        public static IEnumerable<(int row, int col)> GetDiagonalNeighbours<T>(this IList<IList<T>> grid, (int row, int col) position) 
            => RelativeDiagonalNeighbours.GetNeighbours(grid, position);

        public static IEnumerable<(int row, int col)> GetNeighbours<T>(this IList<IList<T>> grid, (int row, int col) position) 
            => RelativeNeighbours.GetNeighbours(grid, position);

        private static IEnumerable<(int row, int col)> GetNeighbours<T>(this IEnumerable<(int row, int col)> relativeNeighbours, IList<IList<T>> grid, (int row, int col) position)
        {
            var (row, col) = position;
            var lastRow = grid.Count - 1;
            var lastCol = grid[0].Count - 1;
            return relativeNeighbours.Select(neighbour => (row: neighbour.row + row, col: neighbour.col + col))
                .Where(x => x.col >= 0 && x.row >= 0 && x.col <= lastCol && x.row <= lastRow);
        }
    }
}