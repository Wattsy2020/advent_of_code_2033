namespace Day3;

public class SchematicPart;

public class Number(int value) : SchematicPart
{
    public readonly int Value = value;
    public bool IsPartNumber = false;
}

public class Symbol(char value, (int, int) cell) : SchematicPart
{
    public readonly char Value = value;
    public readonly (int, int) Cell = cell;
}