using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    public partial class LinkedList<ContentType>
    {
        bool SysCol.ICollection<ContentType>.IsReadOnly => false;

        int SysCol.ICollection<ContentType>.Count => len;

        void SysCol.IList<ContentType>.RemoveAt(int index) => RemoveAt(index);

        void SysCol.ICollection<ContentType>.CopyTo(ContentType[] array, int arrayIndex)
        {
            foreach (ContentType x in this)
                array[arrayIndex++] = x;
        }
    }
}
