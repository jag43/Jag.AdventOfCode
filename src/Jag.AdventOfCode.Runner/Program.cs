using System;
using System.IO;
using System.Threading.Tasks;

namespace Jag.AdventOfCode.Runner
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var solver = new Y2021.Day8.Solver();
            bool test = false;
            int part = 1;

            var input = await new InputRepository().GetInputAsync(solver.Year, solver.Day, test);
            var answer = part == 1 ? solver.SolvePart1(input) : solver.SolvePart2(input);
            
            try
            {
                var expectedAnswer = await new AnswerRepository().GetExpectedAnswer(solver.Year, solver.Day, part, test);
                Console.WriteLine($"{answer} == {expectedAnswer}");
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine(answer);
            }

            return 0;
        }
    }
}