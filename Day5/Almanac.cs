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

    // function that converts the entire array of seeds to the next stage
    // TODO: then consolidates the overlapping ranges
    private static IEnumerable<Range> ConvertRanges(IEnumerable<Range> ranges, ItemMap map) =>
        ranges.SelectMany(map.ConvertToDest);

    // convert the seeds through all the maps
    public IEnumerable<Range> FinalDestinations => _maps.Aggregate(_seeds.AsEnumerable(), ConvertRanges);

    public long Solution2() => FinalDestinations.Min(range => range.Start);
}