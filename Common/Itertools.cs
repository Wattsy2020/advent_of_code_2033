namespace Common;

public static class Itertools
{
    public static string AsString<T>(this IEnumerable<T> enumerable) =>
        string.Join(" ", enumerable.Select(x => x?.ToString() ?? "null"));

    public static int Product(this IEnumerable<int> enumerable) =>
        enumerable.Aggregate(1, (product, value) => product * value);

    public static int Product<T>(this IEnumerable<T> enumerable, Func<T, int> selector) =>
        enumerable.Aggregate(1, (product, value) => product * selector(value));

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
        using var firstEnumerator = first.GetEnumerator();
        using var secondEnumerator = second.GetEnumerator();
        while (true)
        {
            var firstHasItem = firstEnumerator.MoveNext();
            var secondHasItem = secondEnumerator.MoveNext();
            if (!firstHasItem && !secondHasItem) yield break;

            T1? firstItem = firstHasItem ? firstEnumerator.Current : null;
            T2? secondItem = secondHasItem ? secondEnumerator.Current : null;
            yield return combiner(firstItem, secondItem);
        }
    }
}