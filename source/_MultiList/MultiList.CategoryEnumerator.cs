using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    public partial class MultiList<CategoryType, ValueType>
    {
        /// <summary> Iterates over all categories. </summary>
        public SysCol.IEnumerable<CategoryType> EnumerateCategories()
            => new CategoryEnumerator(this);

        sealed class CategoryEnumerator : SysCol.IEnumerable<CategoryType>
        {
            readonly MultiList<CategoryType, ValueType> multiList;

            internal CategoryEnumerator(MultiList<CategoryType, ValueType> multiList)
            {
                this.multiList = multiList;
            }

            IEnumerator IEnumerable.GetEnumerator()
                => ((SysCol.IEnumerable<CategoryType>)this).GetEnumerator();

            SysCol.IEnumerator<CategoryType> SysCol.IEnumerable<CategoryType>.GetEnumerator()
            {
                foreach (SysCol.KeyValuePair<CategoryType, LinkedList<ValueType>> kvp in multiList.data)
                    if (!kvp.Value.empty)
                        yield return kvp.Key;
            }
        }
    }
}
