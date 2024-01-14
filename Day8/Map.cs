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
        // it also would require a lot of brute force to reach the answer without this assumption
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