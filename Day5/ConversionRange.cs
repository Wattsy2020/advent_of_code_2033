namespace Day5;

public class ConversionRange
{
    private readonly long _startDest;
    private readonly long _startSource;
    private readonly long _length;

    private long _endSource => _startSource + _length;

    public ConversionRange(string line)
    {
        var numbers = line.Split(" ").Select(long.Parse).ToList();
        _startDest = numbers[0];
        _startSource = numbers[1];
        _length = numbers[2];
    }

    /// <summary>
    /// Return (Range of what was converted, list of ranges that weren't converted)
    /// </summary>
    public (Range?, List<Range>) ConvertToDest(Range sourceRange)
    {
        Range? converted = null;
        var unconverted = new List<Range>();
        if (sourceRange.Start < _startSource)
            unconverted.Add(sourceRange with { End = Math.Min(_startSource, sourceRange.End) }); // exclusion
        if (sourceRange.Start < _endSource && sourceRange.End > _startSource)
            converted = new Range(
                _startDest + Math.Max(sourceRange.Start, _startSource) - _startSource,
                _startDest + Math.Min(sourceRange.End, _endSource) - _startSource); // intersection
        if (sourceRange.End > _endSource)
            unconverted.Add(sourceRange with { Start = Math.Max(sourceRange.Start, _endSource) }); // exclusion
        return (converted, unconverted);
    }
}