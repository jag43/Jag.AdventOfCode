namespace Jag.AdventOfCode.Y2023.Day8
{
    public record Node(string Name, string Left, string Right)
    {
        public static Node Parse(string input)
        {
            var name = input[..3];
            var left = input[7..10];
            var right = input[12..15];

            return new Node(name, left, right);
        }
    }
}