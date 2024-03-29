namespace Common;

public static class Itertools
{
    public static string AsString<T>(this IEnumerable<T> enumerable, string separator = " ") =>
        string.Join(separator, enumerable.Select(x => x?.ToString() ?? "null"));

    public static int Product(this IEnumerable<int> enumerable) =>
        enumerable.Aggregate(1, (product, value) => product * value);

    public static int Product<T>(this IEnumerable<T> enumerable, Func<T, int> selector) =>
        enumerable.Aggregate(1, (product, value) => product * selector(value));

    public static long Product<T>(this IEnumerable<T> enumerable, Func<T, long> selector) =>
        enumerable.Aggregate(1L, (product, value) => product * selector(value));

    public static IEnumerable<int> Range(int start = 0)
    {
        for (int i = start; true; i++)
            yield return i;
        // ReSharper disable once IteratorNeverReturns (this is meant to be an infinite iterator)
    }

    public static IEnumerable<T> Cycle<T>(this IEnumerable<T> enumerable)
    {
        while (true)
            foreach (var element in enumerable)
                yield return element;
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

    public static (List<T1>, List<T2>) Unpack<T1, T2>(this IEnumerable<(T1, T2)> enumerable)
    {
        List<T1> result1 = new();
        List<T2> result2 = new();
        foreach (var (item1, item2) in enumerable)
        {
            result1.Add(item1);
            result2.Add(item2);
        }

        return (result1, result2);
    }
}