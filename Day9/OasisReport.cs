namespace Day9;

public class OasisReport
{
    private readonly Series[] _reports;

    public OasisReport(IEnumerable<string> reports)
    {
        _reports = reports
            .Select(str => new Series(str.Split(" ").Select(long.Parse)))
            .ToArray();
    }

    public long Solution1() => _reports.Sum(report => report.GetNextValue());

    public long Solution2() => _reports.Sum(report => report.GetPreHistoryValue());
}