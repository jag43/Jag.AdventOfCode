using System.Globalization;
using System.Threading.Tasks;

using Jag.AdventOfCode.Tests.Traits;
using Jag.AdventOfCode.Y2023.Day3;
using Xunit;

namespace Jag.AdventOfCode.Tests.Y2023
{
    [Year(2023), Day(3)]
    public class Day3Tests : TestBase
    {
        public Day3Tests()
        : base (new Solver(), new InputRepository(), new AnswerRepository())
        {
        }

        //[Part(1), Input(true)]
        [Fact]
        public async Task Part1Test()
        {
            await base.Test(solver.Year, solver.Day, 1, true);
        }

        [Fact]
        public async Task Part1TestB()
        {
            string input = @"467#.114.#
                             ...*.....#
                             ..35..633.
                             ......#...
                             617*......
                             .....+.58.
                             ..592.....
                             ......755.
                             ...$.*....
                             .664.598..";
            string expectedAnswer = "4361";
            await base.Test(solver.Year, solver.Day, 1, true, input, expectedAnswer);
        }

        [Fact]
        public async Task Part1TestC()
        {
            string input = @".1.*.";
            string expectedAnswer = "0";
            await base.Test(solver.Year, solver.Day, 1, true, input, expectedAnswer);
        }

        //[Part(1), Input(false)]
        [Fact]
        public async Task Part1()
        {
            await base.Test(solver.Year, solver.Day, 1, false);
        }

        //[Part(2), Input(true)]
        [Fact]
        public async Task Part2Test()
        {
            await base.Test(solver.Year, solver.Day, 2, true);
        }

        //[Part(2), Input(false)]
        [Fact]
        public async Task Part2()
        {
            await base.Test(solver.Year, solver.Day, 2, false);
        }
    }
}