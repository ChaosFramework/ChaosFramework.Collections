using System;

namespace ChaosFramework.Collections
{
    public partial class AdvancedLinkedList<ContentType>
    {
        Node GetCurrentForeachNode(int offset)
        {
            if (enumerators.length < 1)
                throw new InvalidOperationException("No active foreach loop detected.");

            Node n = enumerators[enumerators.length - 1].currentNode;
            if (offset > 0)
                for (int i = 0; i < offset; i++)
                {
                    n++;
                    if (n == null) throw new IndexOutOfRangeException();
                }
            else
                for (int i = 0; i > offset; i--)
                {
                    n--;
                    if (n == null) throw new IndexOutOfRangeException();
                }

            return n;
        }

        /// <summary> Returns an element relative to the current element of the most inner active <see langword="foreach"/> loop. </summary>
        /// <param name="offset"> The relative position of the element to be returned. </param>
        public ContentType GetCurrentForeachElement(int offset = 0)
        {
            Node outNode = enumerators.last.currentNode;
            if (offset == 0)
                return outNode.content;

            if (offset > 0)
                for (int i = 0; i < offset; i++)
                    if (outNode.next == null)
                        return default(ContentType);
                    else
                        outNode = outNode.next;
            else
                for (int i = 0; i > offset; i--)
                    if (outNode.prev == null)
                        return default(ContentType);
                    else
                        outNode = outNode.prev;

            return GetCurrentForeachNode(offset).content;
        }

        /// <summary> Removes an element relative to the current element of the most inner active <see langword="foreach"/> loop. </summary>
        /// <param name="offset">
        ///     The relative position of the element to be removed.
        ///     Defaults to zero, which means the current element itself will be removed.
        /// </param>
        public void RemoveCurrent(int offset = 0)
            => Remove(GetCurrentForeachNode(offset));

        /// <summary>
        ///     Replaces the content of a node relative to the current element
        ///     of the most inner active <see langword="foreach"/> loop.
        /// </summary>
        /// <param name="newContent"> The new content. </param>
        /// <param name="offset">
        ///     The relative position of the element to be replaced.
        ///     Defaults to zero, which means the current element itself will be replaced.
        /// </param>
        public void ReplaceCurrent(ContentType newContent, int offset = 0)
            => GetCurrentForeachNode(offset).content = newContent;

        /// <summary>
        ///     Shifts the current element of the most inner active <see langword="foreach"/> loop
        ///     through the list by a given number of elements.
        /// </summary>
        /// <param name="amount">
        ///     The number of elements to be skipped.
        ///     If negative the current element will be shifted backwards.
        ///     If zero nothing happens.
        /// </param>
        public void ShiftCurrentNode(int amount)
        {
            if (amount != 0)
            {
                int shiftsRemaining = Math.Abs(amount);
                ShiftCurrentNode(amount > 0, _ => shiftsRemaining-- > 0);
            }
        }

        /// <summary>
        ///     Shifts the current element of the most inner active <see langword="foreach"/> loop
        ///     through the list until the given <paramref name="skip"/> function returns false
        ///     or the end of the list is reached.
        /// </summary>
        /// <param name="forward"> Determines whether the element shall be shifted forwards or backwards. </param>
        /// <param name="skip">
        ///     Determines whether the provided element shall be skipped.
        ///     If this returns <see langword="true"/> the provided element will be skipped and
        ///     the next element (depending on <paramref name="forward"/>) will be checked.
        ///     If this returns <see langword="false"/> the shifted node will be placed directly before (if going forwards)
        ///     or after (if going backwards) the provided element.
        /// </param>
        public void ShiftCurrentNode(bool forward, Func<ContentType, bool> skip)
        {
            if (enumerators.empty)
                throw new InvalidOperationException("No current foreach loop detected");

            ListEnumerator currentEnumerator = enumerators[enumerators.length - 1];
            Node currentNode = currentEnumerator.currentNode;
            Node targetNode = forward ? currentNode.next : currentNode.prev;

            while (targetNode != null && skip(targetNode.content))
                targetNode = forward ? targetNode.next : targetNode.prev;

            if (targetNode == null)
            {
                RemoveCurrent();
                if (forward)
                    Add(currentNode);
                else
                    Insert(0, currentNode.content);
            }
            else
            {
                if (forward)
                {
                    if (targetNode.prev == currentNode)
                        return;
                }
                else
                {
                    if (targetNode.next == currentNode)
                        return;
                }

                RemoveCurrent();
                currentNode.removed = false;
                if (forward)
                {
                    currentNode.next = targetNode;
                    currentNode.prev = targetNode.prev;
                    targetNode.prev.next = currentNode;
                    targetNode.prev = currentNode;
                }
                else
                {
                    currentNode.next = targetNode.next;
                    currentNode.prev = targetNode;
                    targetNode.next.prev = currentNode;
                    targetNode.next = currentNode;
                }

                len++;
            }
        }
    }
}
