namespace ChaosFramework.Collections
{
    public partial class LinkedList<ContentType>
    {
        /// <summary> The number of elements in this instance. </summary>
        internal int len;

        /// <summary> The number of elements in this instance. </summary>
        public int length => len;

        /// <summary> Returns true if this <see cref="LinkedList{ContentType}"/> is empty. </summary>
        /// <seealso cref="length"/>
        public bool empty => length == 0;
    }
}
