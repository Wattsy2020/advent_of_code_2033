namespace Common;

public static class Optional
{
    public static T? Max<T>(T? left, T? right)
        where T : struct, IComparable<T> =>
        (left, right) switch
        {
            (null, null) => null,
            (null, T rightVal) => rightVal,
            (T leftVal, null) => leftVal,
            (T leftVal, T rightVal) => (leftVal.CompareTo(rightVal) > 0) ? leftVal : rightVal,
        };

    public static TValue? OptGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        where TValue : struct =>
        dict.TryGetValue(key, out var result) ? result : null;
}