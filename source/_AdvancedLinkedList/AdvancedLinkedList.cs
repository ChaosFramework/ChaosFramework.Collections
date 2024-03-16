using System;
using IEnumerable = System.Collections.IEnumerable;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    /// <summary>
    ///     Advanced version of <see cref="LinkedList{T}"/> which allows manipulating
    ///     the <see cref="SysCol.IEnumerator{T}"/> while traversing the list.
    /// </summary>
    /// <typeparam name="ContentType"> Type of the elements to be listed. </typeparam>
    public partial class AdvancedLinkedList<ContentType>
        : LinkedList<ContentType>,
          IEnumerable,
          SysCol.IEnumerable<ContentType>,
          SysCol.IList<ContentType>,
          SysCol.ICollection<ContentType>
    {
        /// <summary>
        ///     Stores enumerators for each currently active <see langword="foreach"/> loop.
        ///     The last entry describes the most inner loop.
        /// </summary>
        LinkedList<ListEnumerator> enumerators = new LinkedList<ListEnumerator>();

        /// <summary>
        ///     Specifies the enumerator for the next <see langword="foreach"/> loop that is started.
        ///     If <see langword="null"/> a new default enumerator will be created.
        /// </summary>
        ListEnumerator nextEnumerator;

        /// <summary> Creates an empty <see cref="AdvancedLinkedList{ContentType}"/>. </summary>
        public AdvancedLinkedList() : base() { }

        /// <summary> Creates a new <see cref="AdvancedLinkedList{ContentType}"/> with the provided elements. </summary>
        /// <param name="content"> Contains the elements to initialize the list with. </param>
        public AdvancedLinkedList(SysCol.IEnumerable<ContentType> content) : base(content) { }

        /// <summary> Creates a new <see cref="AdvancedLinkedList{ContentType}"/> with the provided elements. </summary>
        /// <param name="content"> Contains the elements to initialize the list with. </param>
        public AdvancedLinkedList(params ContentType[] content) : base(content) { }

        /// <summary> Creates a custom enumerator that is used for the next <see langword="foreach"/> loop. </summary>
        /// <param name="startIndex">
        ///     The current absolute index of the element from where to start the enumeration.
        ///     If the list is changed between setting the enumerator and starting the loop this will not change the starting element.
        /// </param>
        /// <param name="amount">
        ///     The number of elements to be processed.
        ///     If negative the loop will iterate the list backwards.
        ///     If zero the next <see langword="foreach"/> loop will be skipped entirely.
        /// </param>
        public void SetEnumerator(int startIndex, int amount)
        {
            if (amount == 0)
                nextEnumerator = new ListEnumerator(this, null);
            else
                nextEnumerator = new ListEnumerator(
                    this,
                    (startIndex >= length || startIndex < 0) ? null : GetNode(startIndex),
                    amount > 0,
                    Math.Abs(amount)
                    );
        }

        /// <summary>
        ///     Sets the enumerator for the next <see langword="foreach"/> loop relative to an active <see langword="foreach"/> loop.
        ///     Only one enumerator can be set per level of enumeration.
        ///     If called a second time the old enumerator will be replaced.
        ///     This does not manipulate the enumerator of the outer loop in any way.
        ///     <para>
        ///         Default parameters:
        ///         Starts a new <see langword="foreach"/> loop beginning with the
        ///         next element of the next outer <see langword="foreach"/> loop.
        ///     </para>
        /// </summary>
        /// <param name="iterationDepth">
        ///     Specifies to which currently active <see langword="foreach"/> loop this enumerator will be relative.
        ///     Defaults to zero, which means the most inner active <see langword="foreach"/> loop will be used.
        ///     Otherwise this argument specifies the number of active <see langword="foreach"/> loops to be skipped
        ///     in order to reach the loop this enumerator will be relative to.
        /// </param>
        /// <param name="offset">
        ///     The start element of the loop relative to current element of the specified enumerator.
        ///     Defaults to 1, which means the loop will start at the next element of the specified enumerator.
        /// </param>
        /// <param name="amount">
        ///     The number of elements to be processed.
        ///     If negative the loop will iterate the list backwards.
        ///     If zero the next <see langword="foreach"/> loop will be skipped entirely.
        /// </param>
        public void SetSubEnumerator(int iterationDepth = 0, int offset = 1, int amount = int.MaxValue)
        {
            if (enumerators.empty)
                throw new InvalidOperationException("SubEnumerators can only be used during foreach loops");

            bool forward = amount >= 0;
            amount = Math.Abs(amount);

            Node startNode = enumerators[enumerators.length - 1 - iterationDepth].currentNode;

            if (offset > 0)
                for (int i = 0; i < offset; i++)
                    if (startNode.next == null)
                        if (forward)
                        {
                            startNode = null;
                            break;
                        }
                        else
                            amount--;
                    else
                        startNode++;

            else if (offset < 0)
                for (int i = 0; i > offset; i--)
                    if (startNode.prev == null)
                        if (forward)
                            amount--;
                        else
                        {
                            startNode = null;
                            break;
                        }
                    else
                        startNode--;


            nextEnumerator = new ListEnumerator(this, startNode, forward, amount);
        }
    }
}
