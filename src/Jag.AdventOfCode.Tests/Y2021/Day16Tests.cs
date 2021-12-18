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
        [InlineData("8A004A801A8002F478", "16")]
        [InlineData("620080001611562C8802118E34", "12")]
        [InlineData("C0015000016115A2E0802F182340", "23")]
        [InlineData("A0016C880162017C3686B18A3D4780", "31")]
        public void Part1AddtionalTest(string input, string versionSum)
        {
            var answer = solver.SolvePart1(input);

            answer.ShouldBe(versionSum);
        }

        [Theory]
        [InlineData("C200B40A82", "3")]
        [InlineData("04005AC33890", "54")]
        [InlineData("880086C3E88112", "7")]
        [InlineData("CE00C43D881120", "9")]
        [InlineData("D8005AC2A8F0", "1")]
        [InlineData("F600BC2D8F", "0")]
        [InlineData("9C005AC2F8F0", "0")]
        [InlineData("9C0141080250320F1802104A08", "1")]
        public void Part2AddtionalTest(string input, string versionSum)
        {
            var answer = solver.SolvePart2(input);

            answer.ShouldBe(versionSum);
        }

        [Theory]
        [InlineData("110100101111111000101000", true, 6, 2021, 24)]
        [InlineData("11010001010", false, 6, 10, 11)]
        [InlineData("0101001000100100", false, 2, 20, 16)]
        public void Part1AdditionalLiteralPacketTests(string input, bool trim0s, int expectedVersion, int expectedValue, int expectedLength)
        {
            var (packet, length) = Packet.Parse(input, trim0s);

            packet.Version.ShouldBe(expectedVersion);
            packet.Type.ShouldBe(TypeId.Literal);
            length.ShouldBe(expectedLength);

            LiteralPacket literalPacket = (LiteralPacket)packet;
            literalPacket.Value.ShouldBe(expectedValue);
        }

        [Theory]                                                                       //56)]
        [InlineData("00111000000000000110111101000101001010010001001000000000", 1, 6, 2, 49)]
        public void Part1AdditionalOperatorPacketTests(string input, int expectedVersion, int expectedTypeId, int expectedSubPacketCount, int expectedLength)
        {
            var (packet, length) = Packet.Parse(input);

            packet.Version.ShouldBe(expectedVersion);
            ((int)packet.Type).ShouldBe(expectedTypeId);
            packet.Type.ShouldNotBe(TypeId.Literal);
            length.ShouldBe(expectedLength);

            OperatorPacket operatorPacket = (OperatorPacket)packet;
            operatorPacket.SubPackets.Count.ShouldBe(expectedSubPacketCount);
        }
    }
}