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
        public
#if DEBUG
            UnsafeArrayView
#else
            T[]
#endif
            GetUnsafeUnderlyingArray()
#if !DEBUG
            => data;
#else
            => new UnsafeArrayView(data);

        /// <summary>
        ///     Allows unsafe access to the underlying array.
        ///     Asserts that the underlying array wasn't modified.
        /// </summary>
        /// <seealso cref="GetUnsafeUnderlyingArray"/>
        public class UnsafeArrayView
        {
            public readonly T[] data;
            readonly T[] debugReference;

            public UnsafeArrayView(T[] data)
            {
                this.data = data;
                debugReference = new T[data.Length];
                System.Array.Copy(data, debugReference, data.Length);
            }

            ~UnsafeArrayView()
            {
                System.Diagnostics.Debug.Assert(debugReference.SequenceEqual(data));
            }

            public static implicit operator T[] (UnsafeArrayView view) => view.data;
        }
#endif
    }
}
