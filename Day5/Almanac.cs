namespace Day5;

public class Almanac
{
    private readonly long[] _seeds;
    private readonly ItemMap[] _maps;

    public Almanac(string fileContents)
    {
        var sections = fileContents.Split("\n\n");
        _seeds = sections[0].Split(" ").Skip(1).Select(long.Parse).ToArray();
        _maps = sections.Skip(1).Select(section => new ItemMap(section)).ToArray();
    }

    // convert a seed through all the maps
    private long ConvertToDest(long seed) => _maps.Aggregate(seed, (source, itemMap) => itemMap.ConvertToDest(source));

    public IEnumerable<long> Destinations => _seeds.Select(ConvertToDest);

    public long Solution1() => Destinations.Min();
}