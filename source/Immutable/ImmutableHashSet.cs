using SysCol = System.Collections.Generic;

namespace ChaosFramework.Collections.Immutable
{
    public sealed class ImmutableHashSet<Element>
    {
        readonly SysCol.HashSet<Element> set;

        public ImmutableHashSet(SysCol.HashSet<Element> set)
        {
            this.set = set;
        }

        public ImmutableHashSet(SysCol.IEnumerable<Element> set)
        {
            this.set = new SysCol.HashSet<Element>(set);
        }

        public ImmutableHashSet(params Element[] set)
        {
            this.set = new SysCol.HashSet<Element>(set);
        }

        SysCol.IEnumerator<Element> SysCol.IEnumerable<Element>.GetEnumerator()
            => GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => GetEnumerator();

        SysCol.IEnumerator<Element> GetEnumerator()
            => set.GetEnumerator();

        public bool Contains(Element element)
            => set.Contains(element);

        bool IReadonlySet<Element>.Contains(Element element)
            => Contains(element);

        public static implicit operator ImmutableHashSet<Element>(SysCol.HashSet<Element> set)
            => new ImmutableHashSet<Element>(set);
    }
}
