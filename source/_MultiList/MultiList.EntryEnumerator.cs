using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    public partial class MultiList<CategoryType, ValueType>
    {
        /// <summary> Iterates over all entries regardless of category. </summary>
        public SysCol.IEnumerable<ValueType> EnumerateEntries()
            => new EntryEnumerator(this);

        sealed class EntryEnumerator : SysCol.IEnumerable<ValueType>
        {
            readonly MultiList<CategoryType, ValueType> multiList;

            internal EntryEnumerator(MultiList<CategoryType, ValueType> multiList)
            {
                this.multiList = multiList;
            }

            IEnumerator IEnumerable.GetEnumerator()
                => ((SysCol.IEnumerable<ValueType>)this).GetEnumerator();

            SysCol.IEnumerator<ValueType> SysCol.IEnumerable<ValueType>.GetEnumerator()
            {
                foreach (LinkedList<ValueType> lst in multiList.data.Values)
                    foreach (ValueType entry in lst)
                        yield return entry;
            }
        }
    }
}
