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
        // [Fact]
        // public async Task Part1()
        // {
        //     await base.Test(solver.Year, solver.Day, 1, false);
        // }

        // //[Part(2), Input(true)]
        // [Fact]
        // public async Task Part2Test()
        // {
        //     await base.Test(solver.Year, solver.Day, 2, true);
        // }

        // //[Part(2), Input(false)]
        // [Fact]
        // public async Task Part2()
        // {
        //     await base.Test(solver.Year, solver.Day, 2, false);
        // }

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

        [Fact]
        public void ParseTest1()
        {
            string input = "[1,2]";
            var nodes = solver.ParseInput(input);
            var rootNode = nodes.ShouldHaveSingleItem<NumberTreeNode>();
            var rootPair = rootNode.ShouldBeOfType<NumberTreePairNode>();
            rootPair.Left.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(1);
            rootPair.Right.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(2);
        }

        [Fact]
        public void ParseTest2()
        {
            string input = "[[1,2],3]";
            var nodes = solver.ParseInput(input);
            var rootNode = nodes.ShouldHaveSingleItem<NumberTreeNode>();
            var rootPair = rootNode.ShouldBeOfType<NumberTreePairNode>();
            rootPair.Right.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(3);

            var leftPair = rootPair.Left.ShouldBeOfType<NumberTreePairNode>();
            leftPair.Left.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(1);
            leftPair.Right.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(2);
        }

        [Fact]
        public void ParseTest3()
        {
            string input = "[9,[8,7]]";
            var nodes = solver.ParseInput(input);
            var rootNode = nodes.ShouldHaveSingleItem<NumberTreeNode>();
            var rootPair = rootNode.ShouldBeOfType<NumberTreePairNode>();
            rootPair.Left.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(9);

            var rightPair = rootPair.Right.ShouldBeOfType<NumberTreePairNode>();
            rightPair.Left.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(8);
            rightPair.Right.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(7);
        }

        [Fact]
        public void ParseTest4()
        {
            string input = "[[1,9],[8,5]]";
            var nodes = solver.ParseInput(input);
            var rootNode = nodes.ShouldHaveSingleItem<NumberTreeNode>();
            var rootPair = rootNode.ShouldBeOfType<NumberTreePairNode>();

            var leftPair = rootPair.Left.ShouldBeOfType<NumberTreePairNode>();
            leftPair.Left.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(1);
            leftPair.Right.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(9);

            var rightPair = rootPair.Right.ShouldBeOfType<NumberTreePairNode>();
            rightPair.Left.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(8);
            rightPair.Right.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(5);
        }

        [Fact]
        public void ParseTest5()
        {
            string input = "[[[[1,2],[3,4]],[[5,6],[7,8]]],9]";
            var nodes = solver.ParseInput(input);
            var rootNode = nodes.ShouldHaveSingleItem<NumberTreeNode>();
            var rootPair = rootNode.ShouldBeOfType<NumberTreePairNode>();

            var leftNode = rootPair.GetLeftMostNode();
            leftNode.Value.ShouldBe(1);
            leftNode.Parent.Right.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(2);

            var threeFourPair = leftNode.Parent.Parent.Right.ShouldBeOfType<NumberTreePairNode>();
            threeFourPair.Left.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(3);
            threeFourPair.Right.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(4);
            
            rootPair.Right.ShouldBeOfType<NumberTreeNumberNode>().Value.ShouldBe(9);
            var rightNode = rootPair.GetRightMostNode();
            rightNode.Value.ShouldBe(9);

            rootPair.GetNumbersFromLeftToRight().Select(n => n.Value).ShouldBe(new long[] {1, 2, 3, 4, 5, 6, 7, 8, 9});
        }

        [Theory]
        [InlineData("[[[[[9,8],1],2],3],4]", new long[]{ 0, 9, 2, 3, 4})]
        public void ExplodeTests(string input, IEnumerable<long> expectedReducedNumbers)
        {
            var nodes = solver.ParseInput(input);
            var rootNode = nodes[0];
            var leftMost = rootNode.GetLeftMostNode().ShouldBeOfType<NumberTreeNumberNode>();
            var newNode = leftMost.Parent.Explode();
            leftMost.Parent.Parent.Replace(leftMost.Parent, newNode);
            rootNode.GetNumbersFromLeftToRight().Select(n => n.Value).ToList().ShouldBe(expectedReducedNumbers);
        }
    }
}