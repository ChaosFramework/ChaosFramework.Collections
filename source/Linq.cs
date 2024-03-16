using System.Linq;
using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections
{
    /// <summary> Provides some convenience functions not present in <see cref="System.Linq"/>. </summary>
    public static class Linq
    {
        // TODO: move to ChaosUtil

        /// <summary> Meant to return true if the given value fits your desires. </summary>
        public delegate bool Predicate<ContentType>(ContentType obj);

        /// <summary> A non generic predicate that always returns <see langword="true"/>. </summary>
        /// <returns> <see langword="true"/> </returns>
        public static bool PredicateTrue() => true;

        /// <summary> A generic predicate that always returns <see langword="true"/>. </summary>
        /// <typeparam name="T"> The element type the predicate is applied to. </typeparam>
        /// <param name="_"> Irrelevant, as this always returns <see langword="true"/>. </param>
        /// <returns> <see langword="true"/> </returns>
        public static bool PredicateTrue<T>(T _) => true;

        /// <summary> A non generic predicate that always returns <see langword="false"/>. </summary>
        /// <returns> <see langword="false"/> </returns>
        public static bool PredicateFalse() => false;

        /// <summary> A generic predicate that always returns <see langword="false"/>. </summary>
        /// <typeparam name="T"> The element type the predicate is applied to. </typeparam>
        /// <param name="_"> Irrelevant, as this always returns <see langword="false"/>. </param>
        /// <returns> <see langword="false"/> </returns>
        public static bool PredicateFalse<T>(T _) => false;

        /// <summary> Returns whether the given <paramref name="enumerable"/> contains any elements. </summary>
        /// <typeparam name="T"> The element type of the enumerable. </typeparam>
        /// <param name="enumerable"> The enumerable in question. </param>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="enumerable"/> contains any elements;
        ///     <see langword="false"/> if it is empty.
        /// </returns>
        /// <exception cref="System.NullReferenceException"> If <paramref name="enumerable"/> is <see langword="null"/>. </exception>
        public static bool NotEmpty<T>(this SysCol.IEnumerable<T> enumerable) => enumerable.Any(PredicateTrue);

        /// <summary> Returns whether the given <paramref name="enumerable"/> is empty. </summary>
        /// <typeparam name="T"> The element type of the enumerable. </typeparam>
        /// <param name="enumerable"> The enumerable in question. </param>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="enumerable"/> is empty;
        ///     <see langword="false"/> if it contains any elements.
        /// </returns>
        /// <exception cref="System.NullReferenceException"> If <paramref name="enumerable"/> is <see langword="null"/>. </exception>
        public static bool Empty<T>(this SysCol.IEnumerable<T> enumerable) => enumerable.All(PredicateFalse);

        /// <summary> An identity selector. </summary>
        /// <typeparam name="T"> The type of the element to be selcted. </typeparam>
        /// <param name="obj"> The value to be returned. </param>
        /// <returns> The given value passed in <paramref name="obj"/>. </returns>
        public static T SelectIdentity<T>(T obj) => obj;

        /// <summary> Sorts the given <see cref="IEnumerable{T}"/> by one or more keys. </summary>
        /// <typeparam name="Element"> The element type of the <see cref="IEnumerable{T}"/> to be sorted. </typeparam>
        /// <param name="enumerable"> The enumerable to be sorted. </param>
        /// <param name="primarySort"> The comparer representing the primary key used for sorting. </param>
        /// <param name="secondarySorts">
        ///     The comparers representing the secondary keys used for sorting.
        ///     Their priority is the same as their order in this argument.
        /// </param>
        /// <returns> A sorted view of the given <paramref name="enumerable"/>. </returns>
        public static IOrderedEnumerable<Element> OrderByMulti<Element>(
            this SysCol.IEnumerable<Element> enumerable,
            SysCol.IComparer<Element> primarySort,
            params SysCol.IComparer<Element>[] secondarySorts
            ) => OrderByMulti(enumerable, SelectIdentity, primarySort, (SysCol.IEnumerable<SysCol.IComparer<Element>>)secondarySorts);

        /// <summary> Sorts the given <see cref="IEnumerable{T}"/> by one or more keys. </summary>
        /// <typeparam name="Element"> The element type of the <see cref="IEnumerable{T}"/> to be sorted. </typeparam>
        /// <typeparam name="Key"> The type of the keys to be used. </typeparam>
        /// <param name="enumerable"> The enumerable to be sorted. </param>
        /// <param name="keySelector"> A function used to retrieve a sorting key from each of the provided elements. </param>
        /// <param name="primarySort"> The comparer representing the primary key used for sorting. </param>
        /// <param name="secondarySorts">
        ///     The comparers representing the secondary keys used for sorting.
        ///     Their priority is the same as their order in this argument.
        /// </param>
        /// <returns> A sorted view of the given <paramref name="enumerable"/>. </returns>
        public static IOrderedEnumerable<Element> OrderByMulti<Element, Key>(
            this SysCol.IEnumerable<Element> enumerable,
            System.Func<Element, Key> keySelector,
            SysCol.IComparer<Key> primarySort,
            params SysCol.IComparer<Key>[] secondarySorts
            ) => OrderByMulti(enumerable, keySelector, primarySort, (SysCol.IEnumerable<SysCol.IComparer<Key>>)secondarySorts);

        /// <summary> Sorts the given <see cref="IEnumerable{T}"/> by one or more keys. </summary>
        /// <typeparam name="Element"> The element type of the <see cref="IEnumerable{T}"/> to be sorted. </typeparam>
        /// <typeparam name="Key"> The type of the keys to be used. </typeparam>
        /// <param name="enumerable"> The enumerable to be sorted. </param>
        /// <param name="keySelector"> A function used to retrieve a sorting key from each of the provided elements. </param>
        /// <param name="primarySort"> The comparer representing the primary key used for sorting. </param>
        /// <param name="secondarySorts">
        ///     The comparers representing the secondary keys used for sorting.
        ///     Their priority is the same as their order in this argument.
        /// </param>
        /// <returns> A sorted view of the given <paramref name="enumerable"/>. </returns>
        public static IOrderedEnumerable<Element> OrderByMulti<Element, Key>(
            this SysCol.IEnumerable<Element> enumerable,
            System.Func<Element, Key> keySelector,
            SysCol.IComparer<Key> primarySort,
            SysCol.IEnumerable<SysCol.IComparer<Key>> secondarySorts
            )
        {
            IOrderedEnumerable<Element> ordered = enumerable.OrderBy(keySelector, primarySort);
            foreach (SysCol.IComparer<Key> comparer in secondarySorts)
                ordered = ordered.ThenBy(keySelector, comparer);
            return ordered;
        }

        /// <summary> A predicate with two arguments. </summary>
        /// <typeparam name="T1"> The type of the first argument. </typeparam>
        /// <typeparam name="T2"> The type of the second argument. </typeparam>
        /// <param name="a1"> The first argument to the predicate. </param>
        /// <param name="a2"> The second argument to the predicate. </param>
        /// <returns> Returns whether the predicate is satisfied by the provided arguments. </returns>
        public delegate bool Predicate<T1, T2>(T1 a1, T2 a2);

        /// <summary>
        ///     Returns whether any element in a given enumerable satisfies the given predicate using the provided context.
        /// </summary>
        /// <typeparam name="Element"> The element type of the enumerable. </typeparam>
        /// <typeparam name="Context"> The type of context to be used. </typeparam>
        /// <param name="enumerable"> The enumerable to be checked. </param>
        /// <param name="context"> The context on which the predicate depends. </param>
        /// <param name="predicate"> The predicate to be satisfied. </param>
        /// <returns>
        ///     <see langword="true"/> if any element satisfies the predicate;
        ///     <see langword="false"/> otherwise.
        /// </returns>
        public static bool Any<Element, Context>(
            this SysCol.IEnumerable<Element> enumerable,
            Context context,
            Predicate<Element, Context> predicate
            )
        {
            foreach (Element element in enumerable)
                if (predicate(element, context))
                    return true;

            return false;
        }
    }
}
