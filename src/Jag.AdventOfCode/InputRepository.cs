using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Jag.AdventOfCode
{
    public class InputRepository
    {
        public async Task<string> GetInputAsync(int year, int day, bool test)
        {
            string relativePath;
            if (test)
                relativePath = $"{year:d}Day{day:d}Test.txt";
            else
                relativePath = $"{year:d}Day{day:d}.txt";

            var input = await File.ReadAllTextAsync(Path.Join(
                Path.GetDirectoryName(typeof(InputRepository).Assembly.Location),
                Paths.InputRoot,
                relativePath));

            return input;
        }
    }
}