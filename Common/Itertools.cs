namespace Common;

public static class Itertools
{
    public static int Product<T>(this IEnumerable<T> enumerable, Func<T, int> selector)
        => enumerable.Aggregate(1, (product, value) => product * selector(value));

    public static IEnumerable<int> Range(int start = 0)
    {
        for (int i = start; true; i++)
            yield return i;
        // ReSharper disable once IteratorNeverReturns (this is meant to be an infinite iterator)
    }

    public static IEnumerable<(int, T)> Enumerate<T>(this IEnumerable<T> enumerable) => Range().Zip(enumerable);

    public static IEnumerable<TResult> ZipLongest<T1, T2, TResult>(
        this IEnumerable<T1> first,
        IEnumerable<T2> second,
        Func<T1?, T2?, TResult> combiner)
        where T1 : struct
        where T2 : struct
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