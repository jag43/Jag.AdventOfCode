using System.Threading.Tasks;
using Jag.AdventOfCode.Tests;
using Jag.AdventOfCode.Tests.Traits;
using Xunit;

namespace Jag.AdventOfCode.Y2021.Tests
{
    [Year(2021)]
    public class Day7Tests : TestBase
    {
        public Day7Tests()
            : base(new Day7.Solver(), new InputRepository(), new AnswerRepository())
        {
        }

        [Fact, Part(1), Input(true)]
        public override async Task Part1Test()
        {
            await base.Part1Test();
        }
    }
}