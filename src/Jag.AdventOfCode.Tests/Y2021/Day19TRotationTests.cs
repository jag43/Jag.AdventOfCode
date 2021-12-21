using System.Linq;
using System.Threading.Tasks;

using Jag.AdventOfCode.Tests.Traits;
using Jag.AdventOfCode.Y2021.Day19;
using Shouldly;
using Xunit;

namespace Jag.AdventOfCode.Tests.Y2021
{
    [Year(2021), Day(19)]
    public class Day19RotationTests : TestBase
    {
        private new Solver solver => (Solver)base.solver;
        public Day19RotationTests()
        : base (new Solver(), new InputRepository(), new AnswerRepository())
        {
        }

        [Fact]
        public void TestRotations()
        {
            var scanners = solver.ParseInput(input).ToList();
            var scanner1 = scanners.First();
            
            foreach (var s in scanners.Skip(1))
            {
                scanner1.Overlaps(s).ShouldNotBeNull();
            }
        }

        private string input = @"--- scanner 0 ---
-1,-1,1
-2,-2,2
-3,-3,3
-2,-3,1
5,6,-4
8,0,7

--- scanner 0 ---
1,-1,1
2,-2,2
3,-3,3
2,-1,3
-5,4,-6
-8,-7,0

--- scanner 0 ---
-1,-1,-1
-2,-2,-2
-3,-3,-3
-1,-3,-2
4,6,5
-7,0,8

--- scanner 0 ---
1,1,-1
2,2,-2
3,3,-3
1,3,-2
-4,-6,5
7,0,8

--- scanner 0 ---
1,1,1
2,2,2
3,3,3
3,1,2
-6,-4,-5
0,7,-8";
    }
}