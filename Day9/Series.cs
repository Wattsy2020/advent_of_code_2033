namespace Day9;

public class Series
{
    private readonly long[] _series;
    
    public Series(IEnumerable<long> series) => _series = series.ToArray();

    /// <summary>Calculates the delta between each element in the series</summary>
    private Series GetDelta()
    {
        var delta = _series.Zip(_series.Skip(1), (first, second) => second - first);
        return new Series(delta);
    }

    // calculate the next value by recursively calculating delta
    public long GetNextValue()
    {
        // if the series is all 0, there is no change, the next value will also be zero
        if (_series.All(x => x == 0))
            return 0;
        return _series[^1] + GetDelta().GetNextValue();
    }
}