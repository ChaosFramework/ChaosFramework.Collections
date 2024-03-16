using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    public class GenericComparer<T> : SysCol.IEqualityComparer<T>
    {
        public delegate int HashCodeFunc(T obj);

        static int DefaultHashCode(T obj) => obj.GetHashCode();

        readonly Equality<T> equals;
        readonly HashCodeFunc hashCode;

        public GenericComparer(Equality<T> equals, HashCodeFunc hashCode = null)
        {
            this.equals = equals;
            this.hashCode = hashCode ?? DefaultHashCode;
        }

        public bool Equals(T x, T y) => equals(x, y);

        public int GetHashCode(T obj) => hashCode(obj);
    }
}
