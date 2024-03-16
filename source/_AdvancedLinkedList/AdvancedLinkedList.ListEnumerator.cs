using System;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    public partial class AdvancedLinkedList<ContentType>
    {
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetAdvancedEnumerator();
        SysCol.IEnumerator<ContentType> SysCol.IEnumerable<ContentType>.GetEnumerator() => GetAdvancedEnumerator();

        ListEnumerator GetAdvancedEnumerator()
        {
            ListEnumerator enumerator = nextEnumerator ?? new ListEnumerator(this, firstNode);
            nextEnumerator = null;
            enumerators.Add(enumerator);
            return enumerator;
        }

        class ListEnumerator : System.Collections.IEnumerator, SysCol.IEnumerator<ContentType>, IDisposable
        {
            bool dead;
            AdvancedLinkedList<ContentType> list;
            Node start;
            int restCount;
            bool forward;

            public Node currentNode;

            public ListEnumerator(AdvancedLinkedList<ContentType> list, Node start, bool forward = true, int range = int.MaxValue)
            {
                this.list = list;
                this.start = start;
                this.forward = forward;
                restCount = range;
            }

            object System.Collections.IEnumerator.Current => currentNode.content;
            ContentType SysCol.IEnumerator<ContentType>.Current => currentNode.content;

            bool System.Collections.IEnumerator.MoveNext()
            {
                if (dead) return false;

                if (currentNode == null)
                    currentNode = start;
                else
                {
                    if (forward)
                        currentNode++;
                    else
                        currentNode--;
                }

                restCount--;

                return currentNode != null && restCount >= 0;
            }

            void System.Collections.IEnumerator.Reset() => new NotSupportedException();

            void IDisposable.Dispose()
            {
                dead = true;
                currentNode = null;
                restCount = 0;
                list.enumerators.Remove(this);
            }
        }
    }
}
