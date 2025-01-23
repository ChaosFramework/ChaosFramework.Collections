using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections.Immutable
{
    /// <summary>
    ///     An immutable copy of a <see cref="SysCol.HashSet{Element}"/>.
    ///     <para>
    ///         Note:
    ///         While the set itself cannot be changed,
    ///         the elements themselves may remain mutable unless the <typeparamref name="Element"/> type is immutable itself.
    ///     </para>
    /// </summary>
    /// <typeparam name="Element"> The element type of the hashset. </typeparam>
    public sealed class ImmutableHashSet<Element>
        : SysCol.IEnumerable<Element>
    {
        /// <summary> The underlying hashset. </summary>
        readonly SysCol.HashSet<Element> set;

        /// <summary> Creates an immutable copy of the provided <see cref="SysCol.HashSet{Element}"/>. </summary>
        /// <param name="set"> The source data to be contained by this <see cref="ImmutableHashSet{Element}"/>. </param>
        public ImmutableHashSet(SysCol.HashSet<Element> set)
        {
            this.set = new SysCol.HashSet<Element>(set);
        }

        /// <summary>
        ///     Creates an <see cref="ImmutableHashSet{Element}"/> from
        ///     the provided <see cref="SysCol.IEnumerable{Element}"/>.
        /// </summary>
        /// <param name="set"> The source data to be contained by this <see cref="ImmutableHashSet{Element}"/>. </param>
        public ImmutableHashSet(SysCol.IEnumerable<Element> set)
        {
            this.set = new SysCol.HashSet<Element>(set);
        }

        /// <summary> Creates an <see cref="ImmutableHashSet{Element}"/> from the provided array. </summary>
        /// <param name="set"> The source data to be contained by this <see cref="ImmutableHashSet{Element}"/>. </param>
        public ImmutableHashSet(params Element[] set)
        {
            this.set = new SysCol.HashSet<Element>(set);
        }

        SysCol.IEnumerator<Element> SysCol.IEnumerable<Element>.GetEnumerator()
            => GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => GetEnumerator();

        SysCol.IEnumerator<Element> GetEnumerator()
            => set.GetEnumerator();

        /// <summary> Returns whether this instance contains the provided <paramref name="element"/>. </summary>
        /// <param name="element"> The element to be checked for. </param>
        /// <returns>
        ///     <see langword="true"/> if this instance contains <paramref name="element"/>;
        ///     <see langword="false"/> otherwise.
        /// </returns>
        public bool Contains(Element element)
            => set.Contains(element);

        /// <summary> Creates an immutable copy of the provided <paramref name="set"/>. </summary>
        /// <param name="set"> The set to be copied. </param>
        public static implicit operator ImmutableHashSet<Element>(SysCol.HashSet<Element> set)
            => new ImmutableHashSet<Element>(set);
    }
}
