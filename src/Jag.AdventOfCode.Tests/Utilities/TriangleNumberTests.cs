using Jag.AdventOfCode.Utilities;
using Shouldly;
using Xunit;

namespace Jag.AdventOfCode.Tests.Utilities
{
    public class TriangleNumberTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        [InlineData(3, 6)]
        [InlineData(4, 10)]
        [InlineData(5, 15)]
        [InlineData(6, 21)]
        [InlineData(7, 28)]
        [InlineData(8, 36)]
        [InlineData(9, 45)]
        [InlineData(10, 55)]
        [InlineData(55, 1540)]
        [InlineData(100, 5050)]
        [InlineData(256,  32896)]
        public void TriangleNumberTest(int n, int triangleNumber)
        {
            var answer = TriangleNumbers.TriangleNumber(n);

            answer.ShouldBe(triangleNumber);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 2)]
        [InlineData(6, 3)]
        [InlineData(10, 4)]
        [InlineData(15, 5)]
        [InlineData(21, 6)]
        [InlineData(28, 7)]
        [InlineData(36, 8)]
        [InlineData(45, 9)]
        [InlineData(55, 10)]
        [InlineData(1540, 55)]
        [InlineData(5050, 100)]
        [InlineData(32896, 256)]
        public void ReverseTriangleNumberTest(int n, int triangleNumber)
        {
            var answer = TriangleNumbers.ReverseTriangleNumber(n);

            answer.ShouldBe(triangleNumber);
        }
    }
}