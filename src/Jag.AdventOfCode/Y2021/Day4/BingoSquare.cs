namespace Jag.AdventOfCode.Y2021.Day4
{
    public class BingoSquare
    {
        private readonly BingoData bingoData;

        public BingoSquare(BingoData bingoData)
        {
            this.bingoData = bingoData;
        }

        public int Number { get; set; }
        public int Selected => bingoData.Numbers.IndexOf(Number);
    }
}