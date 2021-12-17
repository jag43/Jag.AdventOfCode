using System.Threading.Tasks;

using Jag.AdventOfCode.Tests.Traits;
using Jag.AdventOfCode.Y2021.Day16;
using Shouldly;
using Xunit;

namespace Jag.AdventOfCode.Tests.Y2021
{
    [Year(2021), Day(16)]
    public class Day16Tests : TestBase
    {
        public Day16Tests()
        : base (new Solver(), new InputRepository(), new AnswerRepository())
        {
        }

        private new Solver solver => (Solver)base.solver;

        //[Part(1), Input(true)]
        //[Fact]
        public async Task Part1Test()
        {
            await base.Test(solver.Year, solver.Day, 1, true);
        }

        //[Part(1), Input(false)]
        //[Fact]
        public async Task Part1()
        {
            await base.Test(solver.Year, solver.Day, 1, false);
        }

        //[Part(2), Input(true)]
        // [Fact]
        public async Task Part2Test()
        {
            await base.Test(solver.Year, solver.Day, 2, true);
        }

        //[Part(2), Input(false)]
        // [Fact]
        public async Task Part2()
        {
            await base.Test(solver.Year, solver.Day, 2, false);
        }

        [Theory]
        [InlineData("110100101111111000101000", 6, 2021, 24)]
        public void Part1AdditionalLiteralPacketTests(string input, int expectedVersion, int expectedValue, int expectedLength)
        {
            var (packet, length) = Packet.Parse(input);

            packet.Version.ShouldBe(expectedVersion);
            packet.Type.ShouldBe(TypeId.Literal);
            length.ShouldBe(expectedLength);

            LiteralPacket literalPacket = (LiteralPacket)packet;
            literalPacket.Value.ShouldBe(expectedValue);
        }

        [Theory]
        [InlineData("00111000000000000110111101000101001010010001001000000000", 1, 2021, 24)]
        public void Part1AdditionaOperatorPacketTests(string input, int expectedVersion, int expectedSubPacketCount, int expectedLength)
        {
            var (packet, length) = Packet.Parse(input);

            packet.Version.ShouldBe(expectedVersion);
            packet.Type.ShouldNotBe(TypeId.Literal);
            length.ShouldBe(expectedLength);

            OperatorPacket operatorPacket = (OperatorPacket)packet;
            operatorPacket.SubPackets.Count.ShouldBe(expectedSubPacketCount);
        }
    }
}