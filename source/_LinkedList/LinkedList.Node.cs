namespace ChaosFramework.Collections
{
    public partial class LinkedList<ContentType>
    {
        /// <summary> A single list node that contains information about content and neighbor nodes. </summary>
        internal class Node
        {
            internal bool removed = false;

            /// <summary> The <see cref="Node"/> preceding this <see cref="Node"/>. </summary>
            public Node prev;

            /// <summary> The <see cref="Node"/> following this <see cref="Node"/>. </summary>
            public Node next;

            /// <summary> The content of this <see cref="Node"/>. </summary>
            public ContentType content;

            /// <summary> Creates a new <see cref="Node"/> with content and optional previous and next <see cref="Node"/>s. </summary>
            /// <param name="content"> The content of this <see cref="Node"/>. </param>
            /// <param name="previousNode"> optional: The <see cref="Node"/> preceding this <see cref="Node"/>. </param>
            /// <param name="nextNode"> optional: The <see cref="Node"/> following this <see cref="Node"/>. </param>
            public Node(ContentType content, Node previousNode = null, Node nextNode = null)
            {
                this.content = content;
                prev = previousNode;
                next = nextNode;
            }

            /// <summary>
            ///     Returns the <see cref="Node"/> preceding this <see cref="Node"/>.
            ///     Returns <see langword="null"/>, if there is no preceding <see cref="Node"/>.
            /// </summary>
            public static Node operator --(Node n) => n.prev;

            /// <summary>
            ///     Returns the <see cref="Node"/> following this <see cref="Node"/>.
            ///     Returns <see langword="null"/>, if there is no following <see cref="Node"/>.
            /// </summary>
            public static Node operator ++(Node n) => n.next;

            /// <summary> Returns a human readable representation of this <see cref="Node"/>. </summary>
            public override string ToString()
            {
                System.Text.StringBuilder str = new System.Text.StringBuilder("Content: ");
                str.Append(content.ToString());

                if (prev != null)
                {
                    str.Append("; Previous: ");
                    str.Append(prev.content.ToString());
                }

                if (next != null)
                {
                    str.Append("; Next: ");
                    str.Append(next.content.ToString());
                }

                return str.ToString();
            }
        }
    }
}
