namespace Day5;

public class Range
{
    private readonly long StartDest;
    private readonly long StartSource;
    private readonly long Length;

    public Range(string line)
    {
        var numbers = line.Split(" ").Select(long.Parse).ToList();
        StartDest = numbers[0];
        StartSource = numbers[1];
        Length = numbers[2];
    }

    private bool IsInRange(long sourceItem) => StartSource <= sourceItem && sourceItem < StartSource + Length;

    public long? TryConvertToDest(long sourceItem)
        => IsInRange(sourceItem) ? StartDest + (sourceItem - StartSource) : null;
}