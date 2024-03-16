using System;
using IEnumerable = System.Collections.IEnumerable;
using IEnumerator = System.Collections.IEnumerator;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    public partial class LinkedList<ContentType>
    {
        IEnumerator IEnumerable.GetEnumerator() => new ListEnumerator(firstNode);
        SysCol.IEnumerator<ContentType> SysCol.IEnumerable<ContentType>.GetEnumerator() => new ListEnumerator(firstNode);

        /// <summary> Enumerator that is used for <see langword="foreach"/> loops. </summary>
        class ListEnumerator : IEnumerator, SysCol.IEnumerator<ContentType>
        {
            readonly Node first;

            Node currentNode = null;

            public ListEnumerator(Node first)
            {
                this.first = first;
                currentNode = null;
            }

            bool IEnumerator.MoveNext()
            {
                if (currentNode == null)
                    currentNode = first;
                else
                    currentNode++;

                return currentNode != null;
            }

            void IEnumerator.Reset() => new NotSupportedException();
            object IEnumerator.Current => currentNode.content;
            ContentType SysCol.IEnumerator<ContentType>.Current => currentNode.content;
            void IDisposable.Dispose() { }
        }
    }
}
