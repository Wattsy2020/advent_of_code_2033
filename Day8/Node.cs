namespace Day8;

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