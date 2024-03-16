using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    /// <summary> Encapsulates multiple lists grouped by category. </summary>
    /// <typeparam name="CategoryType"> The type of category to separate lists by. </typeparam>
    /// <typeparam name="ValueType"> The type of data to be stored. </typeparam>
    public partial class MultiList<CategoryType, ValueType> : SysCol.IEnumerable<SysCol.KeyValuePair<CategoryType, LinkedList<ValueType>>>
    {
        // TODO: consistently allow manipulating the list while iterating

        /// <summary> A delegate to determine the category of a <typeparamref name="ValueType"/>. </summary>
        /// <param name="entry"> The <typeparamref name="ValueType"/> whose category is to be determined. </param>
        /// <returns> The determined category. </returns>
        public delegate CategoryType Categorizer(ValueType entry);

        /// <summary> Used <see cref="Categorizer"/> for this instance. </summary>
        public readonly Categorizer categorizer;

        readonly SysCol.Dictionary<CategoryType, LinkedList<ValueType>> data
            = new SysCol.Dictionary<CategoryType, LinkedList<ValueType>>();

        /// <summary> Creates an instance of <see cref="MultiList{CategoryType, ValueType}"/>. </summary>
        /// <param name="categorizer"> The <see cref="Categorizer"/> to be used for this instance. </param>
        public MultiList(Categorizer categorizer) { this.categorizer = categorizer; }

        /// <summary> Adds a <typeparamref name="ValueType"/> to its respective list. </summary>
        /// <param name="entry"> The <typeparamref name="ValueType"/> to be added. </param>
        public void Add(ValueType entry)
        {
            CategoryType cat = categorizer(entry);
            LinkedList<ValueType> lst;
            if (!data.TryGetValue(cat, out lst))
                data[cat] = lst = new LinkedList<ValueType>();

            lst.Add(entry);
        }

        /// <summary> Removes a <typeparamref name="ValueType"/> from its respective list. </summary>
        /// <param name="entry"> The <typeparamref name="ValueType"/> to be removed. </param>
        public void Remove(ValueType entry)
        {
            LinkedList<ValueType> lst;
            if (data.TryGetValue(categorizer(entry), out lst))
                lst.Remove(entry);
        }

        /// <summary> Returns the corresponding <see cref="LinkedList{ValueType}"/> for the given category. </summary>
        /// <param name="category"> The category whose data to retrieve. </param>
        public LinkedList<ValueType> this[CategoryType category] => data[category];

        IEnumerator IEnumerable.GetEnumerator()
            => ((SysCol.IEnumerable<SysCol.KeyValuePair<CategoryType, LinkedList<ValueType>>>)this).GetEnumerator();

        /// <summary> Iterates over all pairs of categories and their content. </summary>
        SysCol.IEnumerator<SysCol.KeyValuePair<CategoryType, LinkedList<ValueType>>>
            SysCol.IEnumerable<SysCol.KeyValuePair<CategoryType, LinkedList<ValueType>>>.GetEnumerator()
        {
            foreach (SysCol.KeyValuePair<CategoryType, LinkedList<ValueType>> kvp in data)
                if (!kvp.Value.empty)
                    yield return kvp;
        }
    }
}
