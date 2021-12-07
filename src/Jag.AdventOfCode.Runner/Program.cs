using System;
using System.Threading.Tasks;
using Jag.AdventOfCode.Tests;

namespace Jag.AdventOfCode.Runner
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var solver = new Y2021.Day3.Solver();
            bool test = false;
            int part = 1;

            var input = await new InputRepository().GetInputAsync(solver.Year, solver.Day, test);

            var expectedAnswer = await new AnswerRepository().GetExpectedAnswer(solver.Year, solver.Day, part, test);
            var answer = part == 1 ? solver.SolvePart1(input) : solver.SolvePart2(input);
            Console.WriteLine($"{answer} == {expectedAnswer}");

            return 0;
        }
    }
}