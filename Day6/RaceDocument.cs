using Common;

namespace Day6;

public class RaceDocument
{
    private const string TimeHeader = "Time:";
    private const string RecordHeader = "Distance:";
    private readonly Race[] _races;

    public RaceDocument(string contents)
    {
        var rows = contents.Split("\n");
        var raceTimes = Parse.ReadNumbers(rows[0][TimeHeader.Length..]);
        var raceRecords = Parse.ReadNumbers(rows[1][RecordHeader.Length..]);
        _races = raceTimes
            .Zip(raceRecords)
            .Select(tuple => new Race(tuple.First, tuple.Second))
            .ToArray();
    }

    public int Solution1() => _races.Product(race => race.NumWinningWays());
}