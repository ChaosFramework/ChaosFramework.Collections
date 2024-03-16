using System;

namespace ChaosFramework.Collections
{
    public partial class LinkedList<ContentType>
    {
        /// <summary>
        ///     Returns whether at least one element in this list satisfies the default comparison
        ///     (<see cref="ContentType.Equals(object)"/>) with the provided <paramref name="obj"/>.
        /// </summary>
        public bool Contains(ContentType obj)
        {
            foreach (ContentType compare in this)
                if (Util.CheckEquality(compare, obj))
                    return true;

            return false;
        }

        /// <summary> Returns whether at least one element in this list satisfies the provided predicate. </summary>
        /// <param name="predicate"> The predicate to be used. </param>
        public bool Contains(Predicate<ContentType> predicate)
        {
            foreach (ContentType obj in this)
                if (predicate(obj))
                    return true;

            return false;
        }

        /// <summary>
        ///     Returns the index of the first element that satisfies the default comparison
        ///     (<see cref="ContentType.Equals(object)"/>) with the provided <paramref name="element"/>.
        /// </summary>
        /// <param name="element"> The element whose index shall be returned. </param>
        public int IndexOf(ContentType element)
        {
            Node n = firstNode;
            for (int i = 0; i < length; i++)
                if (Util.CheckEquality(n.content, element))
                    return i;
                else
                    n++;

            return -1;
        }

        /// <summary> Returns the index of the first element satisfying the provided <paramref name="predicate"/>. </summary>
        /// <param name="predicate"> Predicate that determines whether a specific element is a match. </param>
        public int IndexOf(Predicate<ContentType> predicate)
        {
            Node n = firstNode;
            for (int i = 0; i < length; i++)
                if (predicate(n.content))
                    return i;
                else
                    n++;

            return -1;
        }

        /// <summary>
        ///     Returns the indices of all elements that satisfy the default comparision
        ///     (<see cref="ContentType.Equals(object)"/>) with <param name="element"/> or
        ///     an empty array if no element satisfies the comparison.
        /// </summary>
        public int[] IndicesOf(ContentType element)
        {
            LinkedList<int> lst = new LinkedList<int>();
            int i = 0;
            for (Node n = firstNode; n != null; n++, i++)
                if (Util.CheckEquality(n.content, element))
                    lst.Add(i);

            return lst.ToArray();
        }

        /// <summary>
        ///     Returns the indices of all elements that satisfy the provided <paramref name="predicate"/>
        ///     or an empty array if no element satisfies the predicate.
        /// </summary>
        /// <param name="predicate"> The predicate to be used. </param>
        public int[] IndicesOf(Predicate<ContentType> predicate)
        {
            LinkedList<int> lst = new LinkedList<int>();
            int i = 0;
            for (Node n = firstNode; n != null; n++, i++)
                if (predicate(n.content))
                    lst.Add(i);

            return lst.ToArray();
        }

        /// <summary> Retrieve or set the element with the given index. </summary>
        /// <param name="index"> The index of the desired element. </param>
        /// <exception cref="IndexOutOfRangeException"> Thrown when given an invalid index. </exception>
        public ContentType this[int index]
        {
            get
            {
                Node obj = GetNode(index);
                if (obj != null)
                    return obj.content;
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                Node obj = GetNode(index);
                if (obj != null)
                    obj.content = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary> Returns the <see cref="Node"/> with the given <paramref name="index"/>. </summary>
        /// <param name="index"> The index of the desired <see cref="Node"/>. </param>
        /// <exception cref="IndexOutOfRangeException"> Thrown when given an invalid index. </exception>
        internal Node GetNode(int index)
        {
            if (index < 0 || index >= length)
                throw new IndexOutOfRangeException();

            Node obj = null;
            if (index < length / 2)
            {
                obj = firstNode;
                for (int i = 0; i < index; i++)
                    obj++;
            }
            else
            {
                obj = lastNode;
                for (int i = 0; i < length - index - 1; i++)
                    obj--;
            }

            return obj;
        }
    }
}
