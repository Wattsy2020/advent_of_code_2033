using System.Data;
using Common;

namespace Day8;

public class Map
{
    private readonly string _instructions;
    private readonly Dictionary<string, Node> _network;

    public Map(string contents)
    {
        var parts = contents.Split("\n\n");
        _instructions = parts[0];
        _network = parts[1]
            .Split("\n")
            .Select(Node.ParseNode)
            .ToDictionary();
    }

    public int Solution1()
    {
        var numSteps = 0;
        var currentNode = "AAA";
        foreach (var instruction in _instructions.Cycle())
        {
            if (currentNode == "ZZZ")
                return numSteps;
            currentNode = _network[currentNode].Neighbour(instruction);
            numSteps++;
        }

        throw new ConstraintException("should be impossible to reach here");
    }

    /// <summary>
    /// The number of steps it takes to reach a node ending with Z, and that node's name
    /// </summary>
    private (long, string) NumStepsToZ(string node)
    {
        var numSteps = 0L;
        foreach (var instruction in _instructions.Cycle())
        {
            node = _network[node].Neighbour(instruction);
            numSteps++;
            if (node.EndsWith('Z'))
                return (numSteps, node);
        }

        throw new ConstraintException("should be impossible to reach here");
    }

    /// <summary>
    /// Find the cycle that starts at this node, reaches a node ending in Z, then repeatedly loops back to that Z node
    /// </summary>
    private Cycle CalculateCycle(string node)
    {
        // Note: this assumes that when reaching a Z node, the number of steps must be a multiple of the number of instructions
        // this isn't explicitly stated in the question, but holds for all the examples and the problem input
        var (initialSteps, zNode) = NumStepsToZ(node);
        var (cycleSteps, _) = NumStepsToZ(zNode);
        return new Cycle(initialSteps, cycleSteps);
    }

    public long Solution2()
    {
        // Calculate the cycles, assuming there's a solution then each path must have a cycle
        var currentNodes = _network
            .Select(node => node.Key)
            .Where(node => node.EndsWith('A'))
            .Select(CalculateCycle);

        // Combine the cycles
        var combined = currentNodes.Aggregate((combinedCycle, cycle) => combinedCycle.Combine(cycle));

        // Answer is then the number of initial Steps from the combined cycle
        return combined.InitialSteps;
    }
}

public record Node(string Left, string Right)
{
    public static (string, Node) ParseNode(string line)
    {
        var parts = line.Split(" = ");
        var neighbours = parts[1][1..^1].Split(", ");
        return (parts[0], new Node(neighbours[0], neighbours[1]));
    }

    public string Neighbour(char direction) => direction == 'L' ? Left : Right;
}

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