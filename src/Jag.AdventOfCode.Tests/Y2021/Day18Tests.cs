using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Jag.AdventOfCode.Tests.Traits;
using Jag.AdventOfCode.Y2021.Day18;
using Shouldly;
using Xunit;

namespace Jag.AdventOfCode.Tests.Y2021
{
    [Year(2021), Day(18)]
    public class Day18Tests : TestBase
    {
        public Day18Tests()
        : base (new Solver(), new InputRepository(), new AnswerRepository())
        {
        }

        private new Solver solver => (Solver)base.solver;

        //[Part(1), Input(true)]
        [Fact]
        public async Task Part1Test()
        {
            await base.Test(solver.Year, solver.Day, 1, true);
        }

        //[Part(1), Input(false)]
        [Fact]
        public async Task Part1()
        {
            await base.Test(solver.Year, solver.Day, 1, false);
        }

        //[Part(2), Input(true)]
        [Fact]
        public async Task Part2Test()
        {
            await base.Test(solver.Year, solver.Day, 2, true);
        }

        //[Part(2), Input(false)]
        [Fact]
        public async Task Part2()
        {
            await base.Test(solver.Year, solver.Day, 2, false);
        }

        [Theory]
        [InlineData("[[1,2],[[3,4],5]]", "143")]
        [InlineData("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", "1384")]
        [InlineData("[[[[1,1],[2,2]],[3,3]],[4,4]]", "445")]
        [InlineData("[[[[3,0],[5,3]],[4,4]],[5,5]]", "791")]
        [InlineData("[[[[5,0],[7,4]],[5,5]],[6,6]]", "1137")]
        [InlineData("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", "3488")]
        public void Part1AdditionalTests(string input, string expectedAnswer)
        {
            var answer = solver.SolvePart1(input);

            answer.ShouldBe(expectedAnswer);
        }

        [Theory]
        [InlineData("[1,2]")]
        [InlineData("[[1,2],3]")]
        [InlineData("[9,[8,7]]")]
        [InlineData("[[1,9],[8,5]]")]
        [InlineData("[[[[1,2],[3,4]],[[5,6],[7,8]]],9]")]
        [InlineData("[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]")]
        [InlineData("[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]")]

        public void ParseTests(string input)
        {
            var nodes = solver.ParseInput(input);
            var rootNode = nodes.ShouldHaveSingleItem<NumberTreeNode>();
            
            rootNode.ToString().ShouldBe(input);
        }

        [Theory]
        [InlineData("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
        [InlineData("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
        [InlineData("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
        [InlineData("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
        [InlineData("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
        public void ExplodeTests(string input, string expectedReducedNumbers)
        {
            var nodes = solver.ParseInput(input);
            var rootNode = nodes[0];

            var (node, action) = rootNode.GetNextAction();

            if (action != ReduceAction.Explode)
            {
                throw new System.Exception("Action is not explode");
            }

            var newNode = node.Parent.Explode();
            node.Parent.Parent.Replace(node.Parent, newNode);
            
            rootNode.ToString().ShouldBe(expectedReducedNumbers);
        }

        [Theory]
        [InlineData(10L, "[[5,5],0]")]
        [InlineData(11L, "[[5,6],0]")]
        [InlineData(12L, "[[6,6],0]")]
        public void SplitTests(long left, string expectedReducedNumbers)
        {
            var rootNode = new NumberTreePairNode(left, 0);
            rootNode.Left.Parent = rootNode;
            rootNode.Right.Parent = rootNode;

            var (node, action) = rootNode.GetNextAction();

            if (action != ReduceAction.Split)
            {
                throw new System.Exception("Action is not explode");
            }

            var newNode = node.Split();
            node.Parent.Replace(node, newNode);
            
            rootNode.ToString().ShouldBe(expectedReducedNumbers);
        }
    }
}