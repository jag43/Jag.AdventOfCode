using System.Threading.Tasks;

using Jag.AdventOfCode.Tests.Traits;
using Jag.AdventOfCode.Y2023.Day9;
using Xunit;

namespace Jag.AdventOfCode.Tests.Y2023
{
    [Year(2023), Day(9)]
    public class Day9Tests : TestBase
    {
        public Day9Tests()
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