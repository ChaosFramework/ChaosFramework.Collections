using System;
using Rnd = ChaosUtil.Primitives.Random;

namespace ChaosFramework.Collections
{
    public partial class LinkedList<ContentType>
    {
        static int CompareContentType<T>(ContentType a, ContentType b)
            where T : ContentType, IComparable<ContentType>
            => ((IComparable<ContentType>)a).CompareTo(b);

        /// <summary>
        ///     Inserts an element into the list in a sorted manner.
        ///     <para>
        ///         The new element will be inserted directly before the first element
        ///         where <see cref="System.IComparable{T}.CompareTo(T)"/>
        ///         returns a value less than zero.
        ///     </para>
        /// </summary>
        /// <param name="obj"> The element to be inserted. </param>
        public void AddSorted<T>(T obj)
            where T : ContentType, IComparable<ContentType>
            => AddSorted(obj, CompareContentType<T>);

        /// <summary>
        ///     Inserts an element into the list in a sorted manner.
        ///     <para>
        ///         The new element will be inserted directly before the first element
        ///         where <paramref name="comparison"/> returns a value less than zero.
        ///     </para>
        /// </summary>
        /// <param name="obj"> The element to be inserted. </param>
        /// <param name="comparison"> Determines the desired order of the list. </param>
        public void AddSorted(ContentType obj, Comparison<ContentType> comparison)
        {
            for (Node n = firstNode; n != null; n++)
                if (comparison(obj, n.content) < 0)
                {
                    Node newNode = new Node(obj, n.prev, n);
                    if (n.prev != null)
                        n.prev.next = newNode;
                    else
                        firstNode = newNode;

                    n.prev = newNode;
                    len++;
                    return;
                }

            Add(obj);
        }

        public void Sort<T>()
            where T : ContentType, IComparable<ContentType>
            => Sort(CompareContentType<T>);

        /// <summary> Sorts this instance by the given <see cref="Comparison{ContentType}"/>. </summary>
        /// <param name="comparison"> Determines the desired order of the list. </param>
        public void Sort(Comparison<ContentType> comparison)
        {
            // TODO: sort in place by shifting nodes around, don't be stupid and create a new list.
            LinkedList<ContentType> temp = new LinkedList<ContentType>();
            if (length > 0)
            {
                temp.Add(new Node(this[0]));
                for (Node n = firstNode.next; n != null; n++)
                {
                    bool inserted = false;
                    for (Node t = temp.firstNode; !inserted && t != null; t++)
                        if (comparison(n.content, t.content) < 0)
                        {
                            Node newNode = new Node(n.content, t.prev, t);
                            if (t.prev != null) t.prev.next = newNode;
                            t.prev = newNode;
                            if (temp.firstNode == t) temp.firstNode = newNode;
                            inserted = true;
                            temp.len++;
                        }

                    if (!inserted)
                        temp.Add(new Node(n.content));
                }

                firstNode = temp.firstNode;
                lastNode = temp.lastNode;
            }
        }

        /// <summary> Shuffles this instance so its elements will be in a new random order. </summary>
        public void Shuffle()
        {
            LinkedList<ContentType> list = new LinkedList<ContentType>(this);
            Clear();
            while (list.length > 0)
            {
                Node n = list.GetNode(Rnd.instance.RndInt(list.length));
                list.Remove(n);
                Add(new Node(n.content));
            }
        }
    }
}
