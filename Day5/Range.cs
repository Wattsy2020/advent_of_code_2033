namespace Day5;

/// <summary>
/// Range of [Start, End)
/// </summary>
/// <param name="Start"></param>
/// <param name="End"></param>
public record Range(long Start, long End)
{
    public static Range FromPair(long start, long length) => new Range(start, start + length);
}