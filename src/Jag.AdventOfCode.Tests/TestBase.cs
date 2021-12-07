using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Jag.AdventOfCode;
using System;
using Jag.AdventOfCode.Tests.Traits;

namespace Jag.AdventOfCode.Tests
{
    public abstract class TestBase
    {
        protected readonly ISolver solver;
        protected readonly InputRepository inputRepository;
        protected readonly AnswerRepository answerRepository;

        public TestBase(ISolver solver, InputRepository inputRepository, AnswerRepository answerRepository)
        {
            this.solver = solver;
            this.inputRepository = inputRepository;
            this.answerRepository = answerRepository;
        }

        [Fact, Part(1), Input(true)]
        public virtual async Task Part1Test()
        {
            await Test(solver.Year, solver.Day, 1, true);
        }

        [Fact, Part(1), Input]
        public async Task Part1()
        {
            await Test(solver.Year, solver.Day, 1, false);
        }

        [Fact, Part(2), Input(true)]
        public async Task Part2Test()
        {
            await Test(solver.Year, solver.Day, 2, true);
        }

        [Fact, Part(2), Input]
        public async Task Part2()
        {
            await Test(solver.Year, solver.Day, 2, false);
        }

        protected async Task Test(int year, int day, int part, bool test)
        {
            var input = await inputRepository.GetInputAsync(year, day, test);
            var expectedAnswer = await answerRepository.GetExpectedAnswer(year, day, part, test);

            string answer;
            switch (part)
            {
                case 1:
                    answer = solver.SolvePart1(input);
                    break;
                case 2:
                    answer = solver.SolvePart2(input);
                    break;
                default:
                    throw new Exception($"Invalid part {part}");
            }
            answer.ShouldBe(expectedAnswer);
        }
    }
}