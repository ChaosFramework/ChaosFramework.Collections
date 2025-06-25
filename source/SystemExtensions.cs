using ChaosUtil.Primitives;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    /// <summary> Provides common extensions for various <see cref="System.Collections.IEnumerable"/>s. </summary>
    public static class SystemExtensions
    {
        /// <summary> Returns a random element of the provided <see cref="System.Array"/>. </summary>
        /// <typeparam name="T"> The element type of the <see cref="System.Array"/>. </typeparam>
        /// <param name="array"> The array to be indexed. </param>
        public static T RandomElement<T>(this T[] array) => array[array.RandomIndex()];

        /// <summary> Returns a random index within the boundaries of the provided <see cref="System.Array"/>. </summary>
        /// <typeparam name="T"> The element type of this <see cref="System.Array"/>. </typeparam>
        /// <param name="array"> The array to be indexed. </param>
        public static int RandomIndex<T>(this T[] array) => Random.instance.RndInt(array.Length);

        /// <summary>
        ///     Creates a string containing string representations of all entries of an <see cref="System.Collections.IEnumerable"/>.
        /// </summary>
        /// <param name="enumerable"> The enumerable to be represented. </param>
        /// <param name="separator"> The entry separator to be used. </param>
        /// <param name="nullString"> The string to be used for null entries or enumerables. </param>
        public static string MakeString(
            this System.Collections.IEnumerable enumerable,
            string separator = "; ",
            string nullString = "<null>"
            )
            => enumerable == null ? nullString : $"{{ {string.Join(separator, enumerable.Select(x => x?.ToString() ?? nullString))} }}";

        /// <summary>
        ///     Creates a string containing string representations for a given number
        ///     of entries of an <see cref="System.Collections.IEnumerable"/>.
        /// </summary>
        /// <param name="enumerable"> The enumerable to be represented. </param>
        /// <param name="maxEntries"> The maximum number of entries to be packed into the string. </param>
        /// <param name="separator"> The entry separator to be used. </param>
        /// <param name="nullString"> The string to be used for null entries or enumerables. </param>
        public static string MakeShortString(
            this System.Collections.IEnumerable enumerable,
            int maxEntries,
            string separator = "; ",
            string nullString = "<null>"
            )
            => enumerable == null ? nullString : MakeString(enumerable.Take(maxEntries), separator, nullString);

        // TODO: document this...
        public static bool CompareValueEqualityRecursive(this System.Collections.IEnumerable a, System.Collections.IEnumerable b)
        {
            System.Collections.IEnumerator enumeratorA = a.GetEnumerator();
            System.Collections.IEnumerator enumeratorB = b.GetEnumerator();

            bool aHasNext, bHasNext;
            while ((aHasNext = enumeratorA.MoveNext()) & (bHasNext = enumeratorB.MoveNext()))
            {
                object currentA = enumeratorA.Current;
                object currentB = enumeratorB.Current;

                if (currentA == null && currentB == null) continue;
                if (currentA == null ^ currentB == null) return false;

                if (currentA.GetType() != currentB.GetType())
                    return false;

                System.Collections.IEnumerable enumerableA = currentA as System.Collections.IEnumerable;
                if (enumerableA != null && !(currentA is string)) // strings are IEnumerable, but don't iterate their chars
                {
                    if (!CompareValueEqualityRecursive(enumerableA, (System.Collections.IEnumerable)currentB))
                        return false;
                }
                else
                {
                    if (!currentA.Equals(currentB))
                        return false;
                }
            }

            return !(aHasNext || bHasNext);
        }

        public static System.Collections.IEnumerable Take(this System.Collections.IEnumerable enumerable, int num)
        {
            int i = 0;
            foreach (object obj in enumerable)
                if (i++ < num)
                    yield return obj;
                else
                    break;
        }

        public static SysCol.IEnumerable<TResult> Select<TResult>(
            this System.Collections.IEnumerable enumerable,
            System.Func<object, TResult> selector
            )
        {
            foreach (object element in enumerable)
                yield return selector(element);
        }

        /// <summary>
        ///     Retrieve the value registered for the provided key if it exists
        ///     or create and register a new value using its default constructor.
        /// </summary>
        /// <typeparam name="Key"> The key type of the dictionary. </typeparam>
        /// <typeparam name="Value"> The value type of the dictionary. </typeparam>
        /// <param name="this"> The dictionary instance to retrieve the value from. </param>
        /// <param name="key"> The key whose value to retrieve. </param>
        /// <returns> The existing or newly generated value. </returns>
        public static Value GetOrCreateValue<Key, Value>(this SysCol.IDictionary<Key, Value> @this, Key key)
            where Value : new()
        {
            Value value;
            if (!@this.TryGetValue(key, out value))
                @this[key] = value = new Value();

            return value;
        }
    }
}
