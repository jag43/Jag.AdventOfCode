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

        public abstract long Evaluate();

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

        public override long Evaluate() => Value;
    }

    public class OperatorPacket
        : Packet
    {
        public OperatorPacket(string header, long version, TypeId type)
            : base(header, version, type)
        {
        }

        public List<Packet> SubPackets { get; } = new List<Packet>();

        public override long Evaluate()
        {
            switch(Type)
            {
                case TypeId.Sum:
                    return SubPackets.Sum(p => p.Evaluate());
                case TypeId.Product:
                    return SubPackets.Aggregate(1L, (seed, p) => seed * p.Evaluate());
                case TypeId.Min:
                    return SubPackets.Min(p => p.Evaluate());
                case TypeId.Max:
                    return SubPackets.Max(p => p.Evaluate());
                case TypeId.GreaterThan:
                    if (SubPackets.Count != 2) throw new Exception("GreaterThan packet does not have exactly 2 sub packets.");
                    var gtFirst = SubPackets[0].Evaluate();
                    var gtSecond = SubPackets[1].Evaluate();
                    return gtFirst > gtSecond ? 1 : 0;
                case TypeId.LessThan:
                    if (SubPackets.Count != 2) throw new Exception("GreaterThan packet does not have exactly 2 sub packets.");
                    var ltFirst = SubPackets[0].Evaluate();
                    var ltSecond = SubPackets[1].Evaluate();
                    return ltFirst < ltSecond ? 1 : 0;
                case TypeId.Equals:
                    if (SubPackets.Count != 2) throw new Exception("GreaterThan packet does not have exactly 2 sub packets.");
                    var eqFirst = SubPackets[0].Evaluate();
                    var eqSecond = SubPackets[1].Evaluate();
                    return eqFirst == eqSecond ? 1 : 0;
                default: throw new Exception($"Invalid Type {Type}, header '{Header}'");
            }
        }
    }
}
