using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jag.AdventOfCode.Y2021.Day16
{
    public abstract class Packet
    {
        public static (Packet Packet, int Length) Parse(string binaryString)
        {
            string header = binaryString.Substring(0, 6);
            int version = ConvertFromBinaryString(header.Substring(0, 3));
            TypeId type =(TypeId)ConvertFromBinaryString(header.Substring(3, 3));
            switch (type) 
            {
                case Day16.TypeId.Literal:
                    var sb = new StringBuilder();
                    int currentIndex = 6;
                    string current = binaryString.Substring(currentIndex, 5);
                    while (current[0] == '1')
                    {
                        sb.Append(current.Substring(1, 4));
                        currentIndex += 5;
                        current = binaryString.Substring(currentIndex, 5);
                    }
                    sb.Append(current.Substring(1, 4));
                    var valueString = sb.ToString();
                    var value = ConvertFromBinaryString(valueString);
                    return (new LiteralPacket(header, version, type, value), 6 + currentIndex + 4 - (22 % 5));
                default:
                    // operator
                    var lengthMode = binaryString[6];
                    var lengthLength = lengthMode == '0' ? 15 : 11;
                    var lengthData = ConvertFromBinaryString(binaryString.Substring(7, lengthLength));
                    if (lengthMode == 0)
                    {
                        //string subPackets = binaryString.Substring(8, sub)
                    }
                    break;
            }

            throw new Exception();
        }

        public Packet(string header, int version, TypeId type)
        {
            Header = header;
            Version = version;
            Type = type;
        }

        public string Header { get; }

        public int Version { get; }

        public TypeId Type { get; }

        private static int ConvertFromBinaryString(string binaryString)
        {
            return Convert.ToInt32(binaryString, 2);
        }
    }

    public class LiteralPacket
        : Packet
    {
        public LiteralPacket(string header, int version, TypeId type, int value)
            : base(header, version, type)
        {
            Value = value;
        }

        public int Value { get; }
    }

    public class OperatorPacket
        : Packet
    {
        public OperatorPacket(string header, int version, TypeId type)
            : base(header, version, type)
        {
        }

        public List<Packet> SubPackets { get; } = new List<Packet>();
    }
}
