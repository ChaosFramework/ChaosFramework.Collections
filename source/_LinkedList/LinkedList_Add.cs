using System;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    public partial class LinkedList<ContentType>
    {
        /// <summary>
        ///     Inserts a new element into this <see cref="LinkedList{ContentType}"/>
        ///     at the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> The index, where the new element shall be placed. </param>
        /// <param name="obj"> The element that shall be inserted. </param>
        /// <exception cref="IndexOutOfRangeException"> Thrown when trying to insert at an invalid index. </exception>
        public void Insert(int index, ContentType obj)
        {
            if (index == 0)
                Prepend(obj);
            else if (index == length)
                Add(obj);
            else if (index == length - 1)
            {
                Node n = new Node(obj, lastNode.prev, lastNode);
                lastNode.prev.next = n;
                lastNode.prev = n;
                len++;
            }
            else if (firstNode != null)
            {
                Node item = GetNode(index);
                Node n = new Node(obj, item.prev, item);
                item.prev.next = n;
                item.prev = n;
                len++;
            }
            else
                throw new IndexOutOfRangeException();
        }

        /// <summary> Prepends a new element to the start of the list. </summary>
        /// <param name="content"> The element to be prepended. </param>
        public void Prepend(ContentType content)
        {
            Node n = new Node(content, null, firstNode);
            if (firstNode != null) firstNode.prev = n;
            firstNode = n;
            if (len == 0) lastNode = firstNode;
            len++;
        }

        /// <summary> Adds a new element to the end of this <see cref="LinkedList{ContentType}"/>. </summary>
        /// <param name="obj"> The element to be added. </param>
        public void Add(ContentType obj) => Add(new Node(obj));

        /// <summary> Adds the elements of <paramref name="content"/> to this <see cref="LinkedList{ContentType}"/>. </summary>
        /// <param name="content"> The elements to be added. </param>
        public void Add(params ContentType[] content)
        {
            if (content != null)
                foreach (ContentType val in content)
                    Add(val);
        }

        /// <summary> Adds the elements of <paramref name="content"/> to this <see cref="LinkedList{ContentType}"/>. </summary>
        /// <param name="content"> The elements to be added. </param>
        public void Add(SysCol.IEnumerable<ContentType> content)
        {
            if (content != null)
                foreach (ContentType val in content)
                    Add(val);
        }

        /// <summary> Adds an element to this <see cref="LinkedList{ContentType}"/> only if it's not already in the list. </summary>
        /// <param name="obj"> The element to be added. </param>
        public bool AddUnique(ContentType obj)
        {
            if (Contains(obj))
                return false;

            Add(obj);
            return true;
        }

        /// <summary> Adds all elements in <paramref name="content"/> that are not already in this list. </summary>
        /// <param name="content"> The elements to be added. </param>
        public void AddUnique(SysCol.IEnumerable<ContentType> content)
        {
            foreach (ContentType val in content)
                AddUnique(val);
        }

        /// <summary> Adds a given <see cref="Node"/> to the end of this <see cref="LinkedList{ContentType}"/>. </summary>
        /// <param name="n"> The <see cref="Node"/> to be added. </param>
        internal void Add(Node n)
        {
            n.removed = false;
            if (firstNode == null)
            {
                lastNode = firstNode = n;
                len++;
            }
            else
            {
                n.prev = lastNode;
                n.next = null;
                lastNode.next = n;
                lastNode = n;
                len++;
            }
        }
    }
}
