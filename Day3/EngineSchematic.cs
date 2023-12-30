using Common;

namespace Day3;

public class EngineSchematic
{
    private readonly List<Number> _numbers = new();
    private readonly HashSet<(int, int)> _symbolGrid = new();

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
                if (value != '.')
                    _symbolGrid.Add(currentCell);
            }
        }
    }

    private void AddCurrentNumber(List<char> digits, List<(int, int)> cells)
    {
        if (digits.Count <= 0) return;
        var numberVal = int.Parse(new string(digits.ToArray()));
        var number = new Number(numberVal, cells.ToArray());
        _numbers.Add(number);
        digits.Clear();
        cells.Clear();
    }

    private IEnumerable<Number> PartNumbers() => _numbers.Where(number => number.IsPartNumber(_symbolGrid));

    public int Solution1() => PartNumbers().Sum(number => number.Value);
}