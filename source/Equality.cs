namespace ChaosFramework.Collections
{
    /// <summary> Returns whether the two provided elements are to be treated as equal. </summary>
    public delegate bool Equality<ContentType>(ContentType obj1, ContentType obj2);
}
