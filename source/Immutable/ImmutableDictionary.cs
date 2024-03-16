using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections.Immutable
{
    /// <summary> Represents an immutable copy of a <see cref="SysCol.Dictionary{TKey, TValue}"/>. </summary>
    /// <typeparam name="TKey"> The key type of the dictionary. </typeparam>
    /// <typeparam name="TValue"> The value type of the dictionary. </typeparam>
    public class ImmutableDictionary<TKey, TValue> : SysCol.IEnumerable<SysCol.KeyValuePair<TKey, TValue>>
    {
        readonly SysCol.Dictionary<TKey, TValue> data;

        /// <summary>
        ///     Provides a <see cref="SysCol.Dictionary{TKey, TValue}.KeyCollection"/>
        ///     that contains the keys of this <see cref="ImmutableDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <seealso cref="SysCol.Dictionary{TKey, TValue}.Keys"/>
        public SysCol.Dictionary<TKey, TValue>.KeyCollection keys => data.Keys;

        /// <summary>
        ///     Provides a <see cref="SysCol.Dictionary{TKey, TValue}.ValueCollection"/>
        ///     that contains the values of this <see cref="ImmutableDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <seealso cref="SysCol.Dictionary{TKey, TValue}.Values"/>
        public SysCol.Dictionary<TKey, TValue>.ValueCollection values => data.Values;

        /// <summary> Creates an immutable copy of the given <see cref="SysCol.Dictionary{TKey, TValue}"/>. </summary>
        /// <param name="data"> The data to be made immutable. </param>
        public ImmutableDictionary(SysCol.Dictionary<TKey, TValue> data)
        {
            this.data = new SysCol.Dictionary<TKey, TValue>(data);
        }

        /// <summary> Returns the value associated with the provided <paramref name="key"/>. </summary>
        /// <param name="key"> The key to be used. </param>
        /// <returns> The value associated with the provided <paramref name="key"/>. </returns>
        /// <seealso cref="SysCol.Dictionary{TKey, TValue}.this[TKey]"/>
        public TValue this[TKey key] => data[key];

        /// <summary> Attempt retrieving the value associated with the provided <paramref name="key"/>. </summary>
        /// <param name="key"> The key to be used. </param>
        /// <param name="value">
        ///     Holds the value associated with the given <paramref name="key"/> if successful,
        ///     <see langword="default"/>(<typeparamref name="TValue"/>) otherwise.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the retrieval was successful;
        ///     <see langword="false"/> otherwise.
        /// </returns>
        /// <seealso cref="SysCol.Dictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/>
        public bool TryGetValue(TKey key, out TValue value) => data.TryGetValue(key, out value);

        SysCol.IEnumerator<SysCol.KeyValuePair<TKey, TValue>> SysCol.IEnumerable<SysCol.KeyValuePair<TKey, TValue>>.GetEnumerator()
            => data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => data.GetEnumerator();

        /// <summary> Returns an immutable copy of the provided <see cref="SysCol.Dictionary{TKey, TValue}"/>. </summary>
        /// <param name="data"> The <see cref="SysCol.Dictionary{TKey, TValue}"/> to be wrapped. </param>
        public static implicit operator ImmutableDictionary<TKey, TValue>(SysCol.Dictionary<TKey, TValue> data)
            => new ImmutableDictionary<TKey, TValue>(data);
    }
}
