using System.ComponentModel;
using System.Xml.Serialization;
using Common;

namespace Day10;

// row 0 is the northern-most row
// col 0 is the western-most col
internal record struct Cell(int row, int col)
{
    public static implicit operator Cell((int, int) cellTuple) => new Cell(cellTuple.Item1, cellTuple.Item2);

    public readonly Cell NorthernCell => (row - 1, col);

    public readonly Cell SouthernCell => (row + 1, col);

    public readonly Cell EasternCell => (row, col + 1);

    public readonly Cell WesternCell => (row, col - 1);

    public readonly bool IsNorthOf(Cell other) => this == other.NorthernCell;

    public readonly bool IsSouthOf(Cell other) => this == other.SouthernCell;

    public readonly bool IsEastOf(Cell other) => this == other.EasternCell;

    public readonly bool IsWestOf(Cell other) => this == other.WesternCell;

    public override string ToString() => $"({row}, {col})";
}

public class PipeGrid
{
    private readonly Dictionary<Cell, Pipe> _grid;

    public PipeGrid(IEnumerable<string> inputLines)
    {
        _grid = inputLines.SelectMany((row, rowIdx) =>
                row.Select((c, colIdx) =>
                    new KeyValuePair<Cell, Pipe>((rowIdx, colIdx), PipeUtils.FromChar(c))))
            .ToDictionary();
    }

    private IEnumerable<Cell> ConnectingCells(Cell cell)
    {
        // if the cell to the north connects to it's southern cell (the current `cell`), than it is a connecting cell
        if (_grid.TryGetValue(cell.NorthernCell, out var pipe) && PipeUtils.IsSouthConnecting(pipe))
            yield return cell.NorthernCell;
        if (_grid.TryGetValue(cell.SouthernCell, out pipe) && PipeUtils.IsNorthConnecting(pipe))
            yield return cell.SouthernCell;
        if (_grid.TryGetValue(cell.EasternCell, out pipe) && PipeUtils.IsWestConnecting(pipe))
            yield return cell.EasternCell;
        if (_grid.TryGetValue(cell.WesternCell, out pipe) && PipeUtils.IsEastConnecting(pipe))
            yield return cell.WesternCell;
    }

    private Cell NextLoopCell(Cell prev, Cell current) => _grid[current] switch
    {
        Pipe.NS => prev.IsNorthOf(current) ? current.SouthernCell : current.NorthernCell,
        Pipe.NE => prev.IsNorthOf(current) ? current.EasternCell : current.NorthernCell,
        Pipe.NW => prev.IsNorthOf(current) ? current.WesternCell : current.NorthernCell,
        Pipe.EW => prev.IsEastOf(current) ? current.WesternCell : current.EasternCell,
        Pipe.SW => prev.IsSouthOf(current) ? current.WesternCell : current.SouthernCell,
        Pipe.SE => prev.IsSouthOf(current) ? current.EasternCell : current.SouthernCell,
        _ => throw new InvalidEnumArgumentException()
    };

    // repeatedly get NextLoopCell until reaching the startCell again
    private int LoopLength(Cell startCell, Cell nextLoopCell)
    {
        int numSteps = 0;
        var prevCell = startCell;
        while (nextLoopCell != startCell)
        {
            (prevCell, nextLoopCell) = (nextLoopCell, NextLoopCell(prevCell, nextLoopCell));
            numSteps++;
        }

        return numSteps;
    }

    /// <summary>
    /// Find the size of the loop, from which we can get the number of steps to reach the midpoint
    /// Note this operates under the assumption that each starting Pipe connects to exactly two other pipes,
    /// which always connect to other pipes so as to form a loop back to the starting pipe 
    /// </summary>
    public int Solution1()
    {
        var startingCell = _grid.Single(pair => pair.Value == Pipe.Start).Key;
        var loopCell = ConnectingCells(startingCell).First();
        var loopLength = LoopLength(startingCell, loopCell);
        return (int)Math.Ceiling(loopLength / 2d);
    }
}