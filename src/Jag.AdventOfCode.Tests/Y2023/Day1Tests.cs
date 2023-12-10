using System.Threading.Tasks;

using Jag.AdventOfCode.Tests.Traits;
using Jag.AdventOfCode.Y2023.Day1;
using Shouldly;
using Xunit;

namespace Jag.AdventOfCode.Tests.Y2023
{
    [Year(2023), Day(1)]
    public class Day1Tests : TestBase
    {
        public Day1Tests()
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
            const string part2TestInput = @"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen";
            var expectedAnswer = await answerRepository.GetExpectedAnswerAsync(solver.Year, solver.Day, 2, true);
            var answer = solver.SolvePart2(part2TestInput);
            answer.ShouldBe(expectedAnswer);
        }

        //[Part(2), Input(false)]
        [Fact]
        public async Task Part2()
        {
            await base.Test(solver.Year, solver.Day, 2, false);
        }
    }
}