using IEnumerable = System.Collections.IEnumerable;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    /// <summary>
    ///     A doubly linked list.
    ///     <para>
    ///         Can be changed while iterating with <see langword="foreach"/>.
    ///         Will only process added elements if they're added after the current element.
    ///         Will not process elements that are deleted during iteration.
    ///     </para>
    /// </summary>
    /// <typeparam name="ContentType"> Type of the elements to be listed. </typeparam>
    public partial class LinkedList<ContentType>
        : IEnumerable,
          SysCol.IEnumerable<ContentType>,
          SysCol.IList<ContentType>,
          SysCol.ICollection<ContentType>
    {
        static bool ContentEquals(ContentType a, ContentType b)
            => a.Equals(b);

        /// <summary> The first node of this <see cref="LinkedList{T}"/>. </summary>
        internal Node firstNode;

        /// <summary> The last node of this <see cref="LinkedList{T}"/>. </summary>
        internal Node lastNode;

        /// <summary> Returns the first element of this list. </summary>
        public ContentType first => firstNode.content;

        /// <summary> Returns the last element of this list. </summary>
        public ContentType last => lastNode.content;

        /// <summary> Creates an empty instance of <see cref="LinkedList{ContentType}"/>. </summary>
        public LinkedList() { }

        /// <summary> Creates an instance of <see cref="LinkedList{ContentType}"/> with the given elements. </summary>
        /// <param name="content"> Contains the elements to initialize the list with. </param>
        public LinkedList(params ContentType[] content)
            : this((SysCol.IEnumerable<ContentType>)content)
        { }

        /// <summary> Creates an instance of <see cref="LinkedList{ContentType}"/> with the given elements. </summary>
        /// <param name="content"> Contains the elements to initialize the list with. </param>
        public LinkedList(SysCol.IEnumerable<ContentType> content)
        {
            Add(content);
        }

        /// <summary> Copies the elements of this <see cref="LinkedList{ContentType}"/> to an <see cref="Array"/>. </summary>
        public ContentType[] ToArray()
        {
            if (firstNode == null)
                return new ContentType[0];

            ContentType[] output = new ContentType[length];

            int i = 0;
            for (Node node = firstNode; node != null; node++, i++)
                output[i] = node.content;

            return output;
        }

        /// <summary>
        ///     Compares this list to another one.
        ///     If both lists have the same length and each pair of elements satisfies the
        ///     provided <paramref name="comparer"/> the lists are deemed equal.
        /// </summary>
        /// <param name="compare"> The list to be compared to. </param>
        /// <param name="comparer">
        ///     Defines how to compare contents.
        ///     If <see langword="null"/>  <see cref="object.Equals(object)"/> is used.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the lists have the same length and their elements are deemed equal;
        ///     <see langword="false"/> otherwise.
        /// </returns>
        public bool CompareElements(LinkedList<ContentType> compare, Equality<ContentType> comparer = null)
        {
            if (length != compare.length)
                return false;

            if (comparer == null)
                comparer = ContentEquals;

            Node a = firstNode, b = compare.firstNode;
            while (a != null)
                if (!comparer((a++).content, (b++).content))
                    return false;

            return true;
        }

        /// <summary> Returns a human readable representation of this list's length and elements. </summary>
        public override string ToString()
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder("Length: ");
            output.Append(length);
            output.Append("\n");

            string indexFormat = $"D{(length - 1).ToString().Length}";

            int i = 0;
            for (Node node = firstNode; node != null; node++, i++)
            {
                output.Append("Index: " + i.ToString(indexFormat));
                output.Append("; Content: " + node.content.ToString());
                output.Append("\n");
            }

            return output.ToString();
        }
    }
}
