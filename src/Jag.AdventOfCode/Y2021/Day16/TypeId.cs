namespace Jag.AdventOfCode.Y2021.Day16
{
    public enum TypeId 
    {
        /// <summary>
        /// Packets with type ID 0 are sum packets - their value is the sum of the values of their sub-packets. If they only have a single sub-packet, their value is the value of the sub-packet.
        /// </summary>
        Sum = 0,
        /// <summary>
        /// Packets with type ID 1 are product packets - their value is the result of multiplying together the values of their sub-packets. If they only have a single sub-packet, their value is the value of the sub-packet.
        /// </summary>
        Product = 1,
        /// <summary>
        /// Packets with type ID 2 are minimum packets - their value is the minimum of the values of their sub-packets.
        /// </summary>
        Min = 2,
        /// <summary>
        /// Packets with type ID 3 are maximum packets - their value is the maximum of the values of their sub-packets.
        /// </summary>
        Max = 3,
        /// <summary>
        /// Packets with type ID 4 represent a literal value. Literal value packets encode a single binary number
        /// </summary>
        Literal = 4,
        /// <summary>
        /// Packets with type ID 5 are greater than packets - their value is 1 if the value of the first sub-packet is greater than the value of the second sub-packet; otherwise, their value is 0. These packets always have exactly two sub-packets.
        /// </summary>
        GreaterThan = 5, 
        /// <summary>
        /// Packets with type ID 6 are less than packets - their value is 1 if the value of the first sub-packet is less than the value of the second sub-packet; otherwise, their value is 0. These packets always have exactly two sub-packets.
        /// </summary>
        LessThan = 6,
        /// <summary>
        /// Packets with type ID 7 are equal to packets - their value is 1 if the value of the first sub-packet is equal to the value of the second sub-packet; otherwise, their value is 0. These packets always have exactly two sub-packets.
        /// </summary>
        Equals = 7
    }
}
