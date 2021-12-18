using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jag.AdventOfCode.Y2021.Day16
{
    public abstract class Packet
    {
        public static (Packet Packet, int Length) Parse(string binaryString, bool trim0s = true)
        {
            string header = binaryString.Substring(0, 6);
            var version = ConvertFromBinaryString(header.Substring(0, 3));
            TypeId type =(TypeId)ConvertFromBinaryString(header.Substring(3, 3));
            switch (type) 
            {
                case Day16.TypeId.Literal:
                    var sb = new StringBuilder();
                    int lIndex = 6;
                    string current = binaryString.Substring(lIndex, 5);
                    while (current[0] == '1')
                    {
                        sb.Append(current.Substring(1, 4));
                        lIndex += 5;
                        current = binaryString.Substring(lIndex, 5);
                    }
                    sb.Append(current.Substring(1, 4));
                    lIndex += 5;
                    var valueString = sb.ToString();
                    var value = ConvertFromBinaryString(valueString);
                    var packetLength = lIndex;
                    var leftover = !trim0s ? 0 : 4 - (packetLength % 4);
                    var totalLength = packetLength + leftover;
                    return (new LiteralPacket(header, version, type, value), totalLength);
                default:
                    var packet = new OperatorPacket(header, version, type);
                    var lengthMode = binaryString[6];
                    var lengthLength = lengthMode == '0' ? 15 : 11;
                    var lengthData = ConvertFromBinaryString(binaryString.Substring(7, lengthLength));
                    int oIndex = 7 + lengthLength;
                    if (lengthMode == '0')
                    {
                        while (oIndex < lengthData + 7 + lengthLength)
                        {
                            var (child, length) = Packet.Parse(binaryString.Substring(oIndex), false);
                            packet.SubPackets.Add(child);
                            oIndex += length;
                        }
                    }
                    else if (lengthMode == '1')
                    {
                        for (int i = 0; i < lengthData; i++)
                        {
                            var (child, length) = Packet.Parse(binaryString.Substring(oIndex), false);
                            packet.SubPackets.Add(child);
                            oIndex += length;
                        }
                    }
                    else
                    {
                        throw new Exception($"Invalid lengthMode {lengthMode}");
                    }
                    return (packet, oIndex);
            }

            throw new Exception();
        }

        public Packet(string header, long version, TypeId type)
        {
            Header = header;
            Version = version;
            Type = type;
        }

        public string Header { get; }

        public long Version { get; }

        public TypeId Type { get; }

        public IEnumerable<Packet> FindAllSubPackets(List<Packet> list = null)
        {
            if (list == null) list = new List<Packet>();
            if (this is OperatorPacket packet)
            {
                foreach (var child in packet.SubPackets)
                {
                    list.Add(child);
                    child.FindAllSubPackets(list);
                }
            }
            return list;
        }

        private static long ConvertFromBinaryString(string binaryString)
        {
            return Convert.ToInt64(binaryString, 2);
        }
    }

    public class LiteralPacket
        : Packet
    {
        public LiteralPacket(string header, long version, TypeId type, long value)
            : base(header, version, type)
        {
            Value = value;
        }

        public long Value { get; }
    }

    public class OperatorPacket
        : Packet
    {
        public OperatorPacket(string header, long version, TypeId type)
            : base(header, version, type)
        {
        }

        public List<Packet> SubPackets { get; } = new List<Packet>();
    }
}
