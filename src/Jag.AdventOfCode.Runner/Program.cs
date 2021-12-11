using System;
using System.IO;
using System.Threading.Tasks;

namespace Jag.AdventOfCode.Runner
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var solver = new Y2021.Day11.Solver();
            int part = 2;

            var testInput = await new InputRepository().GetInputAsync(solver.Year, solver.Day, true);
            var input = await new InputRepository().GetInputAsync(solver.Year, solver.Day, false);
            var testAnswer = part == 1 ? solver.SolvePart1(testInput) : solver.SolvePart2(testInput);
            var answer = part == 1 ? solver.SolvePart1(input) : solver.SolvePart2(input);

            try
            {
                var expectedTestAnswer = await new AnswerRepository().GetExpectedAnswer(solver.Year, solver.Day, part, true);
                Console.WriteLine($" TEST: {testAnswer} == {expectedTestAnswer}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($" TEST: {testAnswer}");
            }

            try
            {
                var expectedAnswer = await new AnswerRepository().GetExpectedAnswer(solver.Year, solver.Day, part, false);
                Console.WriteLine($"INPUT: {answer} == {expectedAnswer}");
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine($"INPUT: {answer}");
            }

            return 0;
        }
    }
}