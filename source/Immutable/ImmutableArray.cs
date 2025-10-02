using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;
using SysCol = System.Collections.Generic;
using System.Linq;

namespace ChaosFramework.Collections.Immutable
{
    /// <summary>
    ///     An immutable copy of an array.
    ///     <para>
    ///         Note:
    ///         While the number and order of elements in this array cannot be changed,
    ///         the elements themselves may remain mutable unless the element type is immutable itself.
    ///     </para>
    /// </summary>
    /// <typeparam name="T"> The element type of the array. </typeparam>
    public partial class ImmutableArray<T> : SysCol.IEnumerable<T>, System.IEquatable<ImmutableArray<T>>
    {
        readonly T[] data;

        /// <summary> Constructs an <see cref="ImmutableArray{T}"/> copy of a <see cref="System.Array"/>. </summary>
        /// <param name="data"> The source data to be contained by this <see cref="ImmutableArray{T}"/>. </param>
        public ImmutableArray(T[] data)
        {
            this.data = (T[])data.Clone();
        }

        /// <summary> Returns the array entry at the given index. </summary>
        /// <param name="i"> The index index of the element to be retrieved. </param>
        public T this[int i] => data[i];

        /// <summary> Returns a random element of this <see cref="ImmutableArray{T}"/>. </summary>
        public T RandomEntry() => data.RandomElement();

        /// <summary> Returns the number of elements in this <see cref="ImmutableArray{T}"/>. </summary>
        public int length => data.Length;

        /// <summary> Constructs an <see cref="ImmutableArray{T}"/> copy of a <see cref="System.Array"/>. </summary>
        /// <param name="data"> The source data to be contained by this <see cref="ImmutableArray{T}"/>. </param>
        public static implicit operator ImmutableArray<T>(T[] data) => new ImmutableArray<T>(data);

        /// <summary> Returns a flat copy of this <see cref="ImmutableArray{T}"/>'s data. </summary>
        public T[] ToArray() => (T[])data.Clone();

        IEnumerator IEnumerable.GetEnumerator()
            => ((SysCol.IEnumerable<T>)this).GetEnumerator();

        SysCol.IEnumerator<T> SysCol.IEnumerable<T>.GetEnumerator()
        {
            foreach (T element in data)
                yield return element;
        }

        /// <summary> Returns the index of the first occurence of <paramref name="value"/>. </summary>
        public int IndexOf(T value) => System.Array.IndexOf(data, value);

        public override bool Equals(object obj)
            => Equals(obj as ImmutableArray<T>);

        public bool Equals(ImmutableArray<T> obj)
            => obj != null
            && obj.length == length
            && obj.SequenceEqual(this);

        public override int GetHashCode()
            => data.Length;
    }
}
