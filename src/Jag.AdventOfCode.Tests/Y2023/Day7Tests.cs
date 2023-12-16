using System;
using System.Linq;
using System.Threading.Tasks;

using Jag.AdventOfCode.Tests.Traits;
using Jag.AdventOfCode.Y2023.Day7;
using Xunit;

namespace Jag.AdventOfCode.Tests.Y2023
{
    [Year(2023), Day(7)]
    public class Day7Tests : TestBase
    {
        public Day7Tests()
        : base (new Solver(), new InputRepository(), new AnswerRepository())
        {
        }

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
        [InlineData(["AAAAA", HandType.FiveOfAKind])]
        [InlineData(["AAAA2", HandType.FourOfAKind])]
        [InlineData(["2AAAA", HandType.FourOfAKind])]
        [InlineData(["77888", HandType.FullHouse])]
        [InlineData(["88877", HandType.FullHouse])]
        [InlineData(["67888", HandType.ThreeOfAKind])]
        [InlineData(["88876", HandType.ThreeOfAKind])]
        [InlineData(["77889", HandType.TwoPair])]
        [InlineData(["98877", HandType.TwoPair])]
        [InlineData(["67889", HandType.OnePair])]
        [InlineData(["98876", HandType.OnePair])]
        [InlineData(["23456", HandType.HighCard])]
        [InlineData(["65432", HandType.HighCard])]
        public void Part1HandTypeTests(string rawHand, HandType expectedHandType)
        {
            var hand = Hand1.Parse(rawHand);
            Assert.Equal(expectedHandType, hand.HandType);
        }

        [Fact]
        public void Part1HandOrderingTests()
        {
            Hand1[] expectedOrder = new string[] 
            {
                "AAAAA",
                "AAAA2",
                "2AAAA",
                "88877",
                "77888",
                "88876",
                "67888",
                "98877",
                "77889",
                "98876",
                "67889",
                "65432",
                "23456"
            }
            .Select(Hand1.Parse)
            .ToArray();
            var shuffledHands = expectedOrder.ToArray();
            Random.Shared.Shuffle(shuffledHands);
            var actualOrder = shuffledHands.OrderBy(h => h).ToArray();

            Assert.Equal(expectedOrder, actualOrder);
        }
    
        [Theory]
        [InlineData(["AAAAA", HandType.FiveOfAKind])]
        [InlineData(["AAAAJ", HandType.FiveOfAKind])]
        [InlineData(["AAAA2", HandType.FourOfAKind])]
        [InlineData(["2AAAA", HandType.FourOfAKind])]
        [InlineData(["2JAAA", HandType.FourOfAKind])]
        [InlineData(["77888", HandType.FullHouse])]
        [InlineData(["77J88", HandType.FullHouse])]
        [InlineData(["88877", HandType.FullHouse])]
        [InlineData(["67888", HandType.ThreeOfAKind])]
        [InlineData(["67J88", HandType.ThreeOfAKind])]
        [InlineData(["88876", HandType.ThreeOfAKind])]
        [InlineData(["77889", HandType.TwoPair])]
        [InlineData(["98877", HandType.TwoPair])]
        [InlineData(["67889", HandType.OnePair])]
        [InlineData(["678J9", HandType.OnePair])]
        [InlineData(["98876", HandType.OnePair])]
        [InlineData(["23456", HandType.HighCard])]
        [InlineData(["65432", HandType.HighCard])]
        public void Part2HandTypeTests(string rawHand, HandType expectedHandType)
        {
            var hand = Hand2.Parse(rawHand);
            Assert.Equal(expectedHandType, hand.HandType);
        }
    }
}