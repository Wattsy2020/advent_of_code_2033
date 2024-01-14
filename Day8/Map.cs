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
        var currentNode = "AAA";
        var numSteps = 0;
        foreach (var instruction in _instructions.Cycle())
        {
            if (currentNode == "ZZZ")
                return numSteps;
            currentNode = _network[currentNode].Neighbour(instruction);
            numSteps++;
        }

        throw new ConstraintException("should be impossible to reach here");
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