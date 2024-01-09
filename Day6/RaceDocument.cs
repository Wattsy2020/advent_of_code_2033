using Common;

namespace Day6;

public class RaceDocument
{
    private const string TimeHeader = "Time:";
    private const string RecordHeader = "Distance:";
    private readonly Race[] _races;
    private readonly Race _combinedRace; // for part2

    public RaceDocument(string contents)
    {
        var rows = contents.Split("\n");
        var raceTimes = Parse.ReadNumbers(rows[0][TimeHeader.Length..]).ToList();
        var raceRecords = Parse.ReadNumbers(rows[1][RecordHeader.Length..]).ToList();
        _races = raceTimes
            .Zip(raceRecords)
            .Select(tuple => new Race(tuple.First, tuple.Second))
            .ToArray();

        var raceTime = MathUtils.ConcatLongs(raceTimes.Select(x => (long)x));
        var raceRecord = MathUtils.ConcatLongs(raceRecords.Select(x => (long)x));
        _combinedRace = new Race(raceTime, raceRecord);
    }

    public long Solution1() => _races.Product(race => race.NumWinningWays());

    public long Solution2() => _combinedRace.NumWinningWays();
}