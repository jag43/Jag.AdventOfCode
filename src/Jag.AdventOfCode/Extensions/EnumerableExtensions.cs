using System.Collections.Generic;
using System.Linq;

namespace Jag.AdventOfCode.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T[]> BreakDownIntoWindows<T>(
            this IList<T> sequenceA,
            int windowSize)
        {
            var count = sequenceA.Count();
            for (int i = 0; i <= count - windowSize; i++)
            {
                yield return sequenceA.Skip(i).Take(windowSize).ToArray();
            }
        }

        /// <summary>
        /// Mix the elements of the two sequences, alternating elements
        /// one by one for as long as possible. If one sequence has more
        /// elements than the other, its leftovers will appear at the
        /// end of the mixed sequence.
        /// </summary>
        public static IEnumerable<T> Interleave<T>(
            this IEnumerable<T> sequenceA,
            IEnumerable<T> sequenceB)
        {
            var enumA = sequenceA.GetEnumerator();
            var enumB = sequenceB.GetEnumerator();

            // As long as there are elements in sequence A
            while (enumA.MoveNext())
            {
                // Take an element from sequence A
                yield return enumA.Current;

                // And, if possible,
                if (enumB.MoveNext())
                {
                    // Take an element from sequence B
                    yield return enumB.Current;
                }
            }

            // If there are any elements left over in sequence B
            while (enumB.MoveNext())
            {
                // Take each of them
                yield return enumB.Current;
            }
        }
    }
}