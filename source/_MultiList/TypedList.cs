using SysCol = System.Collections.Generic;
using Type = System.Type;

namespace ChaosFramework.Collections
{
    /// <summary>
    ///     A <see cref="MultiList{CategoryType, ValueType}"/> categorized by its
    ///     <typeparamref name="ValueType"/>'s <see cref="Type"/>.
    /// </summary>
    /// <typeparam name="ValueType"> The type of data to be stored. </typeparam>
    public class TypedList<ValueType> : MultiList<Type, ValueType>
    {
        static Type GetType(ValueType value) => value.GetType();

        /// <summary> Creates a new instance of <see cref="TypedList{ValueType}"/>. </summary>
        public TypedList() : base(GetType) { }

        /// <summary> Iterates over all entries of a given type. </summary>
        /// <typeparam name="CategoryType"> The category to iterate through. </typeparam>
        public SysCol.IEnumerable<CategoryType> EnumerateEntries<CategoryType>()
            where CategoryType : ValueType
        {
            foreach (ValueType entry in EnumerateEntries(typeof(CategoryType)))
                yield return (CategoryType)entry;
        }
    }
}
