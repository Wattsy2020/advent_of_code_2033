using Common;

namespace Day7;

public class HandCollection
{
    private readonly Hand[] _hands;
    private readonly int[] _bids;

    /// <summary>
    /// Create a hand collection from a list of hands and their bids
    /// </summary>
    public HandCollection(string contents)
    {
        var parsed = contents
            .Split("\n")
            .Select(line => line.Split(" "))
            .Select(parts => (new Hand(parts[0]), int.Parse(parts[1])))
            .Unpack();
        _hands = parsed.Item1.ToArray();
        _bids = parsed.Item2.ToArray();
    }

    public override string ToString() =>
        _hands
            .Zip(_bids)
            .Select(parts => $"{parts.Item1} {parts.Item2}")
            .AsString("\n");

    public int Solution() =>
        _hands
            .Zip(_bids)
            .OrderBy(handBids => handBids.First)
            .Select((handBids, rank) => handBids.Second * (rank + 1))
            .Sum();
}