using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Jag.AdventOfCode
{
    public class AnswerRepository
    {
        public async Task<string> GetExpectedAnswer(int year, int day, int part, bool test  = false)
        {
            string relativePath;
            if (test)
                relativePath = $"{year:d}Day{day:d}Part{part:d}Test.txt";
            else
                relativePath = $"{year:d}Day{day:d}Part{part:d}.txt";

            var answer = await File.ReadAllTextAsync(Path.Join(Paths.AnswersRoot, relativePath));

            File.WriteAllText($@"G:\temp\log\{relativePath}", answer);

            return answer;
        }
    }
}