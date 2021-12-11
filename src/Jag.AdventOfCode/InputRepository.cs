using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Jag.AdventOfCode.Config;

namespace Jag.AdventOfCode
{
    public class InputRepository
    {
        private readonly SourceRootConfig sourceRootConfig;

        public InputRepository(SourceRootConfig sourceRootConfig = null)
        {
            this.sourceRootConfig = sourceRootConfig;
        }

        public async Task<string> GetInputAsync(int year, int day, bool test)
        {
            string relativePath = GetRelevantPath(year, day, test);

            try
            {
                var input = await File.ReadAllTextAsync(Path.Join(
                    GetInputDirectory(),
                    relativePath));
                return input;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public async Task CreateInputAsync(int year, int day, bool test, string input)
        {
            string relativePath = GetRelevantPath(year, day, test);

            await File.WriteAllTextAsync(
                Path.Join(
                    GetInputDirectory(),
                    relativePath),
                input);
        }

        private string GetInputDirectory()
        {
            return sourceRootConfig?.InputDirectory
                ?? Path.Join(Path.GetDirectoryName(typeof(AnswerRepository).Assembly.Location), "Input"); ;
        }

        private static string GetRelevantPath(int year, int day, bool test)
        {
            string relativePath;
            if (test)
                relativePath = $"{year:d}Day{day:d}Test.txt";
            else
                relativePath = $"{year:d}Day{day:d}.txt";
            return relativePath;
        }
    }
}