using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Jag.AdventOfCode.Config;

namespace Jag.AdventOfCode
{
    public class AnswerRepository
    {       
        private readonly SourceRootConfig sourceRootConfig;

        public AnswerRepository(SourceRootConfig sourceRootConfig = null)
        {
            this.sourceRootConfig = sourceRootConfig;
        }

        public async Task<string> GetExpectedAnswerAsync(int year, int day, int part, bool test)
        {
            string relativePath = GetRelativePath(year, day, part, test);

            try
            {
                var answer = await File.ReadAllTextAsync(Path.Join(
                    GetAnswersDirectory(),
                    relativePath));
                return answer;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public async Task CreateAnswerAsync(int year, int day, int part, bool test, string answer)
        {
            string relativePath = GetRelativePath(year, day, part, test);

            await File.WriteAllTextAsync(
                Path.Join(GetAnswersDirectory(), relativePath),
                answer);
        }

        private string GetAnswersDirectory()
        {
            return sourceRootConfig?.AnswersDirectory
                ?? Path.Join(Path.GetDirectoryName(typeof(AnswerRepository).Assembly.Location), "Answers"); ;
        }

        private static string GetRelativePath(int year, int day, int part, bool test)
        {
            string relativePath;
            if (test)
                relativePath = $"{year:d}Day{day:d}Part{part:d}Test.txt";
            else
                relativePath = $"{year:d}Day{day:d}Part{part:d}.txt";
            return relativePath;
        }
    }
}