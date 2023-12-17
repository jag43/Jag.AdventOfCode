using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Jag.AdventOfCode.Runner
{
    public class PuzzleService
    {
        private readonly InputRepository inputRepository;
        private readonly AnswerRepository answerRepository;
        private readonly AocHttpClient aocHttpClient;
        private readonly ILogger logger;

        public PuzzleService(InputRepository inputRepository, AnswerRepository answerRepository, AocHttpClient aocHttpClient, ILogger<PuzzleService> logger)
        {
            this.inputRepository = inputRepository;
            this.answerRepository = answerRepository;
            this.aocHttpClient = aocHttpClient;
            this.logger = logger;
        }

        public async Task SolveProblemAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5));

            if (!aocHttpClient.IsConfigured)
            {
                logger.LogWarning("No AoC session cookie - cannot get input or submit results");
            }

            ISolver solver = new Y2023.Day8.Solver();

            var testInput = await inputRepository.GetInputAsync(solver.Year, solver.Day, true);
            var input = await inputRepository.GetInputAsync(solver.Year, solver.Day, false);

            if (aocHttpClient.IsConfigured && string.IsNullOrWhiteSpace(input))
            {
                input = await aocHttpClient.GetInput(solver.Year, solver.Day);
                await inputRepository.CreateInputAsync(solver.Year, solver.Day, false, input); 
            }

            await SolveProblem(solver, 1, input, testInput);
            await SolveProblem(solver, 2, input, testInput);
        }
        
        private async Task SolveProblem(ISolver solver, int part, string input, string testInput)
        {
            var sw = new Stopwatch();
            string testAnswer;
            TimeSpan testTime;
            try
            {
                sw.Start();
                testAnswer = solver.SolvePart(testInput, part);
                testTime = sw.Elapsed;
                sw.Reset();
            }
            catch (NotImplementedException)
            {
                logger.LogInformation($"Part {part} is not implemented.");
                return;
            }

            var expectedTestAnswer = await answerRepository.GetExpectedAnswerAsync(solver.Year, solver.Day, part, true);
            if (expectedTestAnswer != null)
            {
                string correct = GetCorrectString(expectedTestAnswer, testAnswer);
                logger.LogInformation("Test Part {Part}: Answer: '{TestAnswer}' should be '{ExpectedTestAnswer}' " + correct + ", Time {Time} ms", part, testAnswer, expectedTestAnswer, testTime.TotalMilliseconds);
            }
            else
            {
                logger.LogInformation("Test Part {Part}: Answer: '{TestAnswer}', Time {Time} ms", part, testAnswer, testTime.TotalMilliseconds);
            }

            TimeSpan time;
            sw.Start();
            string answer = solver.SolvePart(input, part);
            sw.Stop();
            time = sw.Elapsed;
            testTime = sw.Elapsed;
            var expectedAnswer = await answerRepository.GetExpectedAnswerAsync(solver.Year, solver.Day, part, false);
            if (expectedAnswer != null)
            {
                string correct = GetCorrectString(answer, expectedAnswer);
                logger.LogInformation("     Part {Part}: Answer: '{Answer}' should be '{ExpectedAnswer}' " + correct + ", Time {Time} ms", part, answer, expectedAnswer, time.TotalMilliseconds);
            }
            else
            {
                logger.LogInformation("     Part {Part}: Answer '{Answer}', Time {Time} ms", part, answer, time.TotalMilliseconds);
            }

            if (aocHttpClient.IsConfigured && expectedTestAnswer == testAnswer && string.IsNullOrWhiteSpace(expectedAnswer))
            {
                Console.WriteLine("Submit answer? y/n");
                if (string.Equals(Console.ReadLine(), "y", StringComparison.InvariantCultureIgnoreCase))
                {
                    logger.LogInformation("ExpectedTestAnswer == TestAnswer and no Expected answer found. Submitting actual answer.");
                    var response = await aocHttpClient.SubmitAnswerAsync(solver.Year, solver.Day, part, answer);
                    logger.LogInformation(response.ToMessage(part));
                    if (response == SubmitResponse.Correct)
                    {
                        await answerRepository.CreateAnswerAsync(solver.Year, solver.Day, part, false, answer);
                    }
                }
            }
        }

        private static string GetCorrectString(string answer, string expectedAnswer)
        {
            return expectedAnswer == answer
                ? "✅"
                : "❌";
        }
    }
}