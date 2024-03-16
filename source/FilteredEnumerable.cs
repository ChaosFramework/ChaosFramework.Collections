using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    /// <summary> Provides a blacklist filtered view on a given <see cref="SysCol.IEnumerable{T}"/>. </summary>
    /// <typeparam name="T"> The element type of the filtered <see cref="SysCol.IEnumerable{T}"/>. </typeparam>
    public class FilteredEnumerable<T> : SysCol.IEnumerable<T>
    {
        /// <summary>
        ///     The blacklist used for filtering.
        ///     Entries of this blacklist will be skipped during iteration of this <see cref="FilteredEnumerable{T}"/>.
        /// </summary>
        public readonly SysCol.HashSet<T> exclusions;

        /// <summary>
        ///     The unfiltered base collection.
        ///     For Iteration of this <see cref="FilteredEnumerable{T}"/>
        ///     <see cref="baseEnumerable"/>'s
        ///     <see cref="SysCol.IEnumerable{T}.GetEnumerator"/>
        ///     will be used.
        /// </summary>
        public readonly SysCol.IEnumerable<T> baseEnumerable;

        /// <summary> Creates a new instance of <see cref="FilteredEnumerable{T}"/>. </summary>
        /// <param name="baseEnumerable">
        ///     The base collection used for this <see cref="FilteredEnumerable{T}"/>.
        ///     <para> See <seealso cref="FilteredEnumerable{T}.baseEnumerable"/>. </para>
        /// </param>
        public FilteredEnumerable(SysCol.IEnumerable<T> baseEnumerable)
            : this(baseEnumerable, new SysCol.HashSet<T>())
        { }

        /// <summary> Creates a new instance of <see cref="FilteredEnumerable{T}"/>. </summary>
        /// <param name="baseEnumerable">
        ///     The base collection used for this <see cref="FilteredEnumerable{T}"/>.
        ///     <para> See <seealso cref="FilteredEnumerable{T}.baseEnumerable"/>. </para>
        /// </param>
        /// <param name="exclusions">
        ///     The blacklist used for filtering.
        ///     <para> See <seealso cref="FilteredEnumerable{T}.exclusions"/>. </para>
        /// </param>
        public FilteredEnumerable(SysCol.IEnumerable<T> baseEnumerable, SysCol.HashSet<T> exclusions)
        {
            this.baseEnumerable = baseEnumerable;
            this.exclusions = exclusions;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => ((SysCol.IEnumerable<T>)this).GetEnumerator();

        SysCol.IEnumerator<T> SysCol.IEnumerable<T>.GetEnumerator()
        {
            foreach (T element in baseEnumerable)
                if (!exclusions.Contains(element))
                    yield return element;
        }
    }
}
