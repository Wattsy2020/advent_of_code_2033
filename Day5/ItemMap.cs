namespace Day5;

public class ItemMap
{
    private readonly ConversionRange[] _ranges;

    /// <summary>
    /// expects the lines starting from the "seed-to-soil map:" header to all the range describing lines
    /// </summary>
    public ItemMap(string mapLines)
    {
        _ranges = mapLines
            .Split(":\n")[1]
            .Split("\n")
            .Select(line => new ConversionRange(line))
            .ToArray();
    }

    /// <summary>
    /// Convert a Range through this map, getting the multiple ranges that it is mapped to
    /// If an item isn't listed in the ConversionRanges, then it's conversion is that original item number
    /// </summary>
    public List<Range> ConvertToDest(Range range)
    {
        var converted = new List<Range>();
        var remaining = new List<Range> { range };
        foreach (var conversionRange in _ranges)
        {
            var newRemaining = new List<Range>();
            foreach (var sourceRange in remaining)
            {
                var (conv, unconverted) = conversionRange.ConvertToDest(sourceRange);
                if (conv is not null)
                    converted.Add(conv);
                newRemaining.AddRange(unconverted);
            }

            remaining = newRemaining;
        }

        converted.AddRange(remaining); // ranges not covered by a ConversionRange are unmodified
        return converted;
    }
}