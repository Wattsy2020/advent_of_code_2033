using Common;

namespace Day3;

public class EngineSchematic
{
    private readonly List<SchematicPart> _parts = new();
    private readonly Dictionary<(int, int), SchematicPart> _partGrid = new();

    private IEnumerable<Number> Numbers => _parts.OfType<Number>();
    private IEnumerable<Symbol> Symbols => _parts.OfType<Symbol>();

    public EngineSchematic(string filePath)
    {
        var currentNumberDigits = new List<char>();
        var currentNumberCells = new List<(int, int)>();

        foreach (var (row, line) in File.ReadLines(filePath).Enumerate())
        {
            AddCurrentNumber(currentNumberDigits, currentNumberCells);
            foreach (var (col, value) in line.Enumerate())
            {
                var currentCell = (row, col);
                if (char.IsDigit(value))
                {
                    currentNumberDigits.Add(value);
                    currentNumberCells.Add(currentCell);
                    continue;
                }

                AddCurrentNumber(currentNumberDigits, currentNumberCells);

                if (value == '.') continue;
                var symbol = new Symbol(value, currentCell);
                _parts.Add(symbol);
                _partGrid[currentCell] = symbol;
            }
        }

        UpdatePartNumbers();
    }

    private void AddCurrentNumber(List<char> digits, List<(int, int)> cells)
    {
        if (digits.Count <= 0) return;
        var numberVal = int.Parse(new string(digits.ToArray()));
        var number = new Number(numberVal);
        _parts.Add(number);
        foreach (var cell in cells)
            _partGrid[cell] = number;
        digits.Clear();
        cells.Clear();
    }

    private IEnumerable<(int, int)> AdjacentCells((int, int) cell)
    {
        // row above
        yield return (cell.Item1 - 1, cell.Item2 - 1);
        yield return (cell.Item1 - 1, cell.Item2);
        yield return (cell.Item1 - 1, cell.Item2 + 1);

        // same row
        yield return (cell.Item1, cell.Item2 - 1);
        yield return (cell.Item1, cell.Item2 + 1);

        // row below
        yield return (cell.Item1 + 1, cell.Item2 - 1);
        yield return (cell.Item1 + 1, cell.Item2);
        yield return (cell.Item1 + 1, cell.Item2 + 1);
    }


    private void UpdatePartNumbers()
    {
        foreach (var cell in Symbols.SelectMany(symbol => AdjacentCells(symbol.Cell)))
        {
            if (_partGrid.TryGetValue(cell, out var part) && part is Number num)
                num.IsPartNumber = true;
        }
    }

    public int Solution1() => Numbers.Where(num => num.IsPartNumber).Sum(num => num.Value);
}