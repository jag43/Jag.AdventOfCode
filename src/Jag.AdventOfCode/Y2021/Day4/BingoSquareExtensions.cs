using System.Collections.Generic;
using System.Linq;

namespace  Day4
{
    public static class BingoSquareExtensions
    {
        public static int AchievesBingo(this List<BingoSquare> squares)
        {
            return squares.Max(sq => sq.Selected);
        }
    }
}