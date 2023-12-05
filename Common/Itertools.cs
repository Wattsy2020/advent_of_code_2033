namespace Common;

public static class Itertools
{
    public static IEnumerable<TResult> ZipLongest<T1, T2, TResult>(
        this IEnumerable<T1> first,
        IEnumerable<T2> second, 
        Func<T1?, T2?, TResult> combiner)
        where T1: struct
        where T2: struct
    {
        using var firstEnum = first.GetEnumerator();
        using var secondEnum = second.GetEnumerator();
        var firstHasItem = false;
        var secondHasItem = false;
        while ((firstHasItem = firstEnum.MoveNext()) || (secondHasItem = secondEnum.MoveNext()))
        {
            T1? firstItem = firstHasItem ? firstEnum.Current : null;
            T2? secondItem = secondHasItem ? secondEnum.Current : null;
            yield return combiner(firstItem, secondItem);
        }
    }
}