using Common;

namespace Day8;

public record Cycle(long InitialSteps, long CycleSteps)
{
    private long InitialMeetingPoint(Cycle other)
    {
        // Add cycles to the cycle at the lower position, until they meet
        var cycles = 0;
        var otherCycles = 0;
        long pos;
        long otherPos;
        while ((pos = Position()) != (otherPos = OtherPosition()))
        {
            if (pos < otherPos)
                cycles++;
            else
                otherCycles++;
        }

        return pos;

        long Position() => InitialSteps + cycles * CycleSteps;
        long OtherPosition() => other.InitialSteps + otherCycles * other.CycleSteps;
    }

    // The combined cycle will start when both cycle first meet
    // and both cycles will then meet at the lowest common multiple of the cycle steps
    // Note this assumes cycles combine at somepoint, e.g. Cycle(1, 2) and Cycle(0, 2) never combine
    public Cycle Combine(Cycle other) =>
        new(InitialMeetingPoint(other), MathUtils.LCM(CycleSteps, other.CycleSteps));
}