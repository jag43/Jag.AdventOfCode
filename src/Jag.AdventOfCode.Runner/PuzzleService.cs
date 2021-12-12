using System;
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

            ISolver solver = new Y2020.Day2.Solver();

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
            string testAnswer;
            try
            {
                testAnswer = solver.SolvePart(testInput, part);
            }
            catch (NotImplementedException)
            {
                logger.LogInformation($"Part {part} is not implemented.");
                return;
            }

            var expectedTestAnswer = await answerRepository.GetExpectedAnswerAsync(solver.Year, solver.Day, part, true);
            if (expectedTestAnswer != null)
            {
                logger.LogInformation("Test Part {Part}: Expected {ExpectedTestAnswer}, Actual: {TestAnswer}", part, expectedTestAnswer, testAnswer);
            }
            else
            {
                logger.LogInformation("Test Part {Part}: Expected FILE_NOT_FOUND, Actual: {TestAnswer}", part, testAnswer);
            }

            string answer = solver.SolvePart(input, part);
            var expectedAnswer = await answerRepository.GetExpectedAnswerAsync(solver.Year, solver.Day, part, false);
            if (expectedAnswer != null)
            {
                logger.LogInformation("Part {Part}: Expected {ExpectedAnswer}, Actual: {Answer}", part, expectedAnswer, answer);
            }
            else
            {
                logger.LogInformation("Part {Part}: Expected FILE_NOT_FOUND, Actual: {Answer}", part, answer);
            }

            if (aocHttpClient.IsConfigured && expectedTestAnswer == testAnswer && string.IsNullOrWhiteSpace(expectedAnswer))
            {
                Console.WriteLine("Submit answer? y/n");
                if (string.Equals(Console.ReadLine(), "y", StringComparison.InvariantCultureIgnoreCase))
                {
                    logger.LogInformation("ExpectedTestAnswer == TestAnswer and no Expected answer found. Submitting actual answer.");
                    var response = await aocHttpClient.SubmitAnswerAsync(solver.Year, solver.Day, part, answer);
                    logger.LogInformation(response.ToMessage());
                    if (response == SubmitResponse.Correct)
                    {
                        await answerRepository.CreateAnswerAsync(solver.Year, solver.Day, part, false, answer);
                    }
                }
            }
        }
    }
}