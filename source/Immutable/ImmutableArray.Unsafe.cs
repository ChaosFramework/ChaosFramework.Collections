using System.Linq;

namespace ChaosFramework.Collections.Immutable
{
    partial class ImmutableArray<T>
    {
        /// <summary>
        ///     Returns a direct reference to the underlying array.
        ///     Meant only for use with API that does not accept
        ///     immutable arrays and copying it is unacceptable.
        /// </summary>
        /// <remarks>
        ///     In debug mode this will assert that the underlying array is unmodified.
        ///
        ///     Modifications to a copied reference can't be detected after
        ///     the <see cref="UnsafeArrayView"/> object ran out of scope.
        /// </remarks>
        public UnsafeArrayView GetUnsafeUnderlyingArray()
            => new UnsafeArrayView(data);

        /// <summary>
        ///     Allows unsafe access to the underlying array.
        ///     Asserts that the underlying array wasn't modified.
        /// </summary>
        /// <seealso cref="GetUnsafeUnderlyingArray"/>
        public class UnsafeArrayView
        {
            public readonly T[] data;
#if DEBUG
            readonly T[] debugReference;
#endif

            public UnsafeArrayView(T[] data)
            {
                this.data = data;
#if DEBUG
                debugReference = new T[data.Length];
                System.Array.Copy(data, debugReference, data.Length);
#endif
            }

#if DEBUG
            ~UnsafeArrayView()
            {
                System.Diagnostics.Debug.Assert(debugReference.SequenceEqual(data));
            }
#endif

            public static implicit operator T[] (UnsafeArrayView view) => view.data;
        }
    }
}
