using Common;

namespace Day3;

public class SchematicPart;

public class Number(int value) : SchematicPart
{
    public readonly int Value = value;
    public bool IsPartNumber = false;
}

public class Symbol(char value, (int, int) cell) : SchematicPart
{
    public readonly (int, int) Cell = cell;
    public readonly HashSet<Number> AdjacentNumbers = new();

    public bool IsGear => value == '*' && AdjacentNumbers.Count == 2;
    public int GearRatio => AdjacentNumbers.Product(number => number.Value);
}