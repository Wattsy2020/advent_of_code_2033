namespace Day5;

public class Almanac
{
    private readonly Range[] _seeds;
    private readonly ItemMap[] _maps;

    public Almanac(string fileContents)
    {
        var sections = fileContents.Split("\n\n");
        _seeds = sections[0]
            .Split(" ")
            .Skip(1) // skip the "seeds: " label
            .Select(long.Parse)
            .Chunk(2)
            .Select(nums => Range.FromPair(nums[0], nums[1]))
            .ToArray();
        _maps = sections.Skip(1).Select(section => new ItemMap(section)).ToArray();
    }

    // Consolidate any overlapping ranges
    private static IEnumerable<Range> MergeRanges(IEnumerable<Range> ranges)
    {
        Range? prevRange = null;
        foreach (var range in ranges.OrderBy(range => range.Start))
        {
            if (prevRange is null)
            {
                prevRange = range;
                continue;
            }

            // if overlapping, then merge
            if (range.Start <= prevRange.End)
            {
                prevRange = prevRange with { End = Math.Max(prevRange.End, range.End) };
                continue;
            }

            yield return prevRange;
            prevRange = range;
        }

        if (prevRange is not null)
            yield return prevRange;
    }

    // function that converts the entire array of seeds to the next stage
    private static IEnumerable<Range> ConvertRanges(IEnumerable<Range> ranges, ItemMap map) =>
        MergeRanges(ranges.SelectMany(map.ConvertToDest));

    // convert the seeds through all the maps
    public IEnumerable<Range> FinalDestinations => _maps.Aggregate(_seeds.AsEnumerable(), ConvertRanges);

    public long Solution2() => FinalDestinations.Min(range => range.Start);
}