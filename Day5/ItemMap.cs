namespace Day5;

public class ItemMap
{
    private readonly Range[] _ranges;

    /// <summary>
    /// expects the lines starting from the "seed-to-soil map:" header to all the range describing lines
    /// </summary>
    public ItemMap(string mapLines)
    {
        _ranges = mapLines
            .Split(":\n")[1]
            .Split("\n")
            .Select(line => new Range(line))
            .ToArray();
    }

    /// <summary>
    /// Convert an item from the source list to the destination list
    /// If an item isn't listed in the ranges, then it's conversion is that original item number
    /// </summary>
    public long ConvertToDest(long sourceItem)
        => _ranges
            .Select(range => range.TryConvertToDest(sourceItem))
            .OfType<long>()
            .FirstOrDefault(sourceItem);
}