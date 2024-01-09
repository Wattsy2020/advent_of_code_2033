namespace Day6;

// Given a race taking time S
// Since your speed k = the number of seconds the button is held for
// Then the distance travelled in the race = k(s - k) = sk - k^2
// This is an upside down parabola, so we know it has a maximum somewhere in the middle, and only decreases either side of that maximum
// So we can use binary search to find the boundaries where distance < _recordDistance, which would optimise the below code

public class Race(long raceTime, long recordDistance)
{
    private long RaceDistance(long k) => k * (raceTime - k);

    private bool IsWinningK(long k) => RaceDistance(k) > recordDistance;

    /// <summary>
    /// find a race winning point (using binary search)
    /// assumes there is a winning point
    /// </summary>
    private long FindWinningK(long low, long high)
    {
        var midPoint = low + (high - low) / 2;
        var raceDistance = RaceDistance(midPoint);
        if (raceDistance > recordDistance)
            return midPoint;
        else if (RaceDistance(midPoint - 1) > raceDistance) // increasing to the left
            return FindWinningK(low, midPoint);
        return FindWinningK(midPoint + 1, high);
    }

    /// <summary>
    /// find the boundaries on either side (also using binary search)
    /// specifically by searching for a winning k, that is directly neighbouring a losing k
    /// </summary>
    /// <returns>The boundary k that is the leftmost winning k (or rightmost for findLeftBoundary=false)</returns>
    private long FindBoundary(long low, long high, bool findLeftBoundary)
    {
        var midPoint = low + (high - low) / 2;
        if (!IsWinningK(midPoint)) // too low, need to find a winning value of k
            return findLeftBoundary
                ? FindBoundary(midPoint + 1, high, findLeftBoundary)
                : FindBoundary(low, midPoint, findLeftBoundary);
        var neighbor = findLeftBoundary ? midPoint - 1 : midPoint + 1;
        if (!IsWinningK(neighbor))
            return midPoint;

        // neighbor is still winning, need to search k that will give a lower distance
        return findLeftBoundary
            ? FindBoundary(low, midPoint, findLeftBoundary)
            : FindBoundary(midPoint + 1, high, findLeftBoundary);
    }

    public long NumWinningWays()
    {
        var winningK = FindWinningK(0, raceTime);
        var leftBoundary = FindBoundary(0, winningK, true);
        var rightBoundary = FindBoundary(winningK + 1, raceTime, false);
        return rightBoundary - leftBoundary + 1;
    }
}