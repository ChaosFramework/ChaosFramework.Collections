using IEnumerable = System.Collections.IEnumerable;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    /// <summary> Provides several utility functions related to collections and enumerables. </summary>
    public static class Util
    {
        /// <summary> Delegate that assigns a weight to a given element. </summary>
        public delegate float GetWeight<T>(T element);

        /// <summary>
        ///     Returns a random element from <paramref name="lst"/> respecting the weights assigned by <paramref name="weight"/>.
        ///     <para> Depending on the value of <paramref name="rndValue"/> it returns the following: </para>
        ///     <para> &#x2022;            <paramref name="rndValue"/> &lt;     0: <paramref name="fallback"/> </para>
        ///     <para> &#x2022; 0 &#x2264; <paramref name="rndValue"/> &lt;     1: a weighted element          </para>
        ///     <para> &#x2022;            <paramref name="rndValue"/> &#x2265; 1: <paramref name="fallback"/> </para>
        /// </summary>
        /// <typeparam name="T"> The element type of the enumeration. </typeparam>
        /// <param name="lst"> The enumeration from which to select. </param>
        /// <param name="weight">
        ///     Function that assigns a weight to a given element.
        ///     Only called once, therefore does not need to be pure.
        /// </param>
        /// <param name="rndValue"> Determines which element is to be returned. </param>
        /// <param name="fallback"> The value to return when no suitable element can be determined. </param>
        public static T GetRandomWeightedEntry<T>(IEnumerable lst, GetWeight<T> weight, float rndValue, T fallback = default(T))
        {
            if (rndValue < 0 || rndValue >= 1.0f)
                return fallback;

            LinkedList<System.Tuple<T, float>> weightedEntries = new LinkedList<System.Tuple<T, float>>();
            float sum = 0;
            foreach (T entry in lst)
            {
                float weightValue = weight(entry);
                if (weightValue > 0)
                {
                    weightedEntries.Add(new System.Tuple<T, float>(entry, weightValue));
                    sum += weightValue;
                }
            }

            if (sum == 0)
                return fallback;

            float rndVal = rndValue * sum;
            foreach (System.Tuple<T, float> entry in weightedEntries)
            {
                rndVal -= entry.Item2;
                if (rndVal < 0)
                    return entry.Item1;
            }

            return fallback;
        }

        /// <summary> Default comparison for two nullable objects. </summary>
        public static bool CheckEquality<ContentType>(ContentType a, ContentType b)
        {
            // TODO: move to ChaosUtil
            if (a == null ^ b == null) return false;
            if (a == null && b == null) return true;
            return a.Equals(b);
        }

        /// <summary>
        ///     Flattens a sequence of enumerables to a single enumerable.
        ///     Null entries in <paramref name="enumerables"/> will be skipped.
        /// </summary>
        /// <typeparam name="Element"> The element type of the given enumerables. </typeparam>
        /// <param name="enumerables"> The enumerables to flatten. </param>
        /// <returns> The flattened sequence. </returns>
        public static SysCol.IEnumerable<Element> EnumerateMany<Element>(params SysCol.IEnumerable<Element>[] enumerables)
        {
            foreach (SysCol.IEnumerable<Element> enumerable in enumerables)
                if (enumerable != null)
                    foreach (Element element in enumerable)
                        yield return element;
        }

        /// <summary> Create an <see cref="IEnumerable{T}"/> containing only a single element. </summary>
        /// <typeparam name="T"> The type of the <paramref name="element"/> and <see cref="IEnumerable{T}"/>. </typeparam>
        /// <param name="element"> The sole element in the resulting <see cref="IEnumerable{T}"/>. </param>
        /// <returns> An <see cref="IEnumerable{T}"/> containing only <paramref name="element"/>. </returns>
        public static SysCol.IEnumerable<T> Yield<T>(T element)
        {
            yield return element;
        }
    }
}
