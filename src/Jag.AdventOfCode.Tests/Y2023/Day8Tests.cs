using System.Threading.Tasks;

using Jag.AdventOfCode.Tests.Traits;
using Jag.AdventOfCode.Y2023.Day8;
using Xunit;

namespace Jag.AdventOfCode.Tests.Y2023
{
    [Year(2023), Day(8)]
    public class Day8Tests : TestBase
    {
        public Day8Tests()
        : base (new Solver(), new InputRepository(), new AnswerRepository())
        {
        }

        //[Part(1), Input(true)]
        [Fact]
        public async Task Part1Test()
        {
            await base.Test(solver.Year, solver.Day, 1, true);
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
            string inputOverride = @"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)";
            await base.Test(solver.Year, solver.Day, 2, true, inputOverride, "6");
        }

        //[Part(2), Input(false)]
        [Fact(Skip = "true")]
        public async Task Part2()
        {
            await base.Test(solver.Year, solver.Day, 2, false);
        }
    }
}