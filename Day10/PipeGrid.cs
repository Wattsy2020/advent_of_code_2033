namespace Day10;

internal record struct Cell(int row, int col);

public class PipeGrid
{
    private readonly Dictionary<Cell, Pipe> _grid;

    public PipeGrid(IEnumerable<string> inputLines)
    {
        _grid = inputLines.SelectMany((row, rowIdx) =>
                row.Select((c, colIdx) =>
                    new KeyValuePair<Cell, Pipe>(new Cell(rowIdx, colIdx), PipeConstruct.FromChar(c))))
            .ToDictionary();
    }

    // Do BFS traversal from both sides until they meet each other, return the number of steps it took to meet
    public int Solution1()
    {
        return 0;
    }
}