using System;

namespace ChaosFramework.Collections
{
    public partial class LinkedList<ContentType>
    {
        /// <summary> Removes the element with the given index from this instance and returns it. </summary>
        /// <param name="index"> The index of the element to be removed. </param>
        /// <exception cref="IndexOutOfRangeException"> Thrown when given an invalid index. </exception>
        public ContentType RemoveAt(int index)
        {
            if (index >= length || index < 0)
                throw new IndexOutOfRangeException();

            if (index == 0)
            {
                ContentType returnValue = firstNode.content;
                firstNode = firstNode.next;
                if (firstNode != null)
                    firstNode.prev = null;
                else
                    lastNode = null;

                len--;
                return returnValue;
            }
            else if (index == length - 1)
            {
                ContentType returnValue = lastNode.content;
                lastNode = lastNode.prev;
                lastNode.next = null;
                len--;
                return returnValue;
            }

            Node n = GetNode(index);
            Remove(n);
            return n.content;
        }

        /// <summary>
        ///     Removes the first element that satisfies the default comparison
        ///     (<see cref="ContentType.Equals(object)"/>) with the provided <paramref name="element"/>.
        /// </summary>
        /// <param name="element"> The object to be removed. </param>
        /// <returns>
        ///     <see langword="true"/> if a matching element was found and removed;
        ///     <see langword="false"/> otherwise.
        /// </returns>
        public bool Remove(ContentType element)
        {
            for (Node n = firstNode; n != null; n++)
                if (Util.CheckEquality(n.content, element))
                {
                    Remove(n);
                    return true;
                }

            return false;
        }

        /// <summary>
        ///     Removes all elements that satisfy the default comparison
        ///     (<see cref="ContentType.Equals(object)"/>) with the provided <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj"> The object to be removed. </param>
        public void RemoveAll(ContentType obj)
        {
            for (Node n = firstNode; n != null; n++)
                if (n.content.Equals(obj))
                    Remove(n);
        }

        /// <summary> Removes a specific node from this <see cref="LinkedList{ContentType}"/>. </summary>
        /// <param name="n"> The node to be removed. </param>
        internal void Remove(Node n)
        {
            if (n.removed)
                return;

            n.removed = true;

            if (n.prev != null) n.prev.next = n.next;
            if (n.next != null) n.next.prev = n.prev;

            if (n == lastNode) lastNode = n.prev;
            if (n == firstNode) firstNode = n.next;

            len--;
        }

        /// <summary> Removes a given range of elements from this instance. </summary>
        /// <param name="startIndex"> The index of the first element to be removed. </param>
        /// <param name="range"> The number of elements to be removed. </param>
        /// <exception cref="IndexOutOfRangeException"> Thrown when given an invalid index or range. </exception>
        public void RemoveRange(int startIndex, int range)
        {
            if (range <= 0)
                return;

            if (startIndex < 0 || startIndex + range > length)
                throw new IndexOutOfRangeException();

            Node firstOneToDelete = GetNode(startIndex);
            Node lastOneToDelete = firstOneToDelete;

            for (int i = 1; i < range && lastOneToDelete.next != null; i++)
                lastOneToDelete++;

            if (firstOneToDelete.prev != null) firstOneToDelete.prev.next = lastOneToDelete.next;
            if (lastOneToDelete.next != null) lastOneToDelete.next.prev = firstOneToDelete.prev;

            for (Node n = firstOneToDelete; n != lastOneToDelete.next;)
            {
                n.prev = firstOneToDelete.prev;
                Node temp = n.next;
                n.next = lastOneToDelete.next;
                n = temp;
            }

            if (startIndex == 0) firstNode = lastOneToDelete.next;
            if (startIndex + range >= len) lastNode = firstOneToDelete.prev;

            len -= range;
        }

        /// <summary> Removes all elements from this instance. </summary>
        public void Clear()
        {
            firstNode = null;
            lastNode = null;
            len = 0;
        }
    }
}
