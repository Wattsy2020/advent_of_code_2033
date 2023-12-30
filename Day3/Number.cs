namespace Day3;

public record Number(int Value, (int, int)[] Cells)
{
    private static bool ContainsAdjacent((int, int) cell, HashSet<(int, int)> symbolGrid) =>
        // row above
        symbolGrid.Contains((cell.Item1 - 1, cell.Item2 - 1)) ||
        symbolGrid.Contains((cell.Item1 - 1, cell.Item2)) ||
        symbolGrid.Contains((cell.Item1 - 1, cell.Item2 + 1)) ||

        // same row
        symbolGrid.Contains((cell.Item1, cell.Item2 - 1)) ||
        symbolGrid.Contains((cell.Item1, cell.Item2 + 1)) ||

        // row below
        symbolGrid.Contains((cell.Item1 + 1, cell.Item2 - 1)) ||
        symbolGrid.Contains((cell.Item1 + 1, cell.Item2)) ||
        symbolGrid.Contains((cell.Item1 + 1, cell.Item2 + 1));

    public bool IsPartNumber(HashSet<(int, int)> symbolGrid)
        => Cells.Any(cell => ContainsAdjacent(cell, symbolGrid));
}