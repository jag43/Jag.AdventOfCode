using System;
using System.Text;

namespace Jag.AdventOfCode.Y2021.Day16
{
    public class Solver : ISolver
    {
        public int Year => 2021;

        public int Day => 16;

        public string SolvePart1(string input)
        {
            var packet = ParseInput(input);
            return packet.Packet.Version.ToString();       
        }

        public string SolvePart2(string input)
        {
            throw new NotImplementedException();
        }

        public (Packet Packet, int Length) ParseInput(string input)
        {
            var sb = new StringBuilder();
            foreach (var c in input)
            {
                sb.Append(c switch {
                    '0' => "0000",
                    '1' => "0001",
                    '2' => "0010",
                    '3' => "0011",
                    '4' => "0100",
                    '5' => "0101",
                    '6' => "0110",
                    '7' => "0111",
                    '8' => "1000",
                    '9' => "1001",
                    'A' => "1010",
                    'B' => "1011",
                    'C' => "1100",
                    'D' => "1101",
                    'E' => "1110",
                    'F' => "1111",
                    _ => throw new Exception("Invalid input")
                });
            }
            var binaryString = sb.ToString();

            return Packet.Parse(binaryString);
        }


        
    }
}