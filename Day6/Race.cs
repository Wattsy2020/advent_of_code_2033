namespace Day6;

// Given a race taking time S
// Since your speed k = the number of seconds the button is held for
// Then the distance travelled in the race = k(s - k) = sk - k^2
// This is an upside down parabola, so we know it has a maximum somewhere in the middle, and only decreases either side of that maximum
// So we can use binary search to find the boundaries where distance < _recordDistance, which would optimise the below code

public class Race(int raceTime, int recordDistance)
{
    public int NumWinningWays() =>
        Enumerable
            .Range(0, raceTime + 1)
            .Count(k => k * (raceTime - k) > recordDistance);
}