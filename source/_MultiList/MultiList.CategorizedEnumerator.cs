using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    public partial class MultiList<CategoryType, ValueType>
    {
        /// <summary> Iterates over all entries of a given category. </summary>
        public SysCol.IEnumerable<ValueType> EnumerateEntries(CategoryType category)
            => new CategorizedEnumerator(this, category);

        sealed class CategorizedEnumerator : SysCol.IEnumerable<ValueType>
        {
            readonly MultiList<CategoryType, ValueType> multiList;
            readonly CategoryType category;

            internal CategorizedEnumerator(MultiList<CategoryType, ValueType> multiList, CategoryType category)
            {
                this.multiList = multiList;
                this.category = category;
            }

            IEnumerator IEnumerable.GetEnumerator()
                => ((SysCol.IEnumerable<ValueType>)this).GetEnumerator();

            SysCol.IEnumerator<ValueType> SysCol.IEnumerable<ValueType>.GetEnumerator()
            {
                LinkedList<ValueType> lst;
                if (multiList.data.TryGetValue(category, out lst))
                    foreach (ValueType entry in lst)
                        yield return entry;
            }
        }
    }
}
