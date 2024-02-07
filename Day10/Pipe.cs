using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Day10;

/// <summary>
/// | is a vertical pipe connecting north and south.
/// - is a horizontal pipe connecting east and west.
/// L is a 90-degree bend connecting north and east.
/// J is a 90-degree bend connecting north and west.
/// 7 is a 90-degree bend connecting south and west.
/// F is a 90-degree bend connecting south and east.
/// . is ground; there is no pipe in this tile.
/// S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum Pipe
{
    NS = 0,
    EW = 1,
    NE = 2,
    NW = 3,
    SW = 4,
    SE = 5,
    Ground = 6,
    Start = 7,
}

public static class PipeConstruct
{
    public static Pipe FromChar(char pipeStr) => pipeStr switch
    {
        '|' => Pipe.NS,
        '-' => Pipe.EW,
        'L' => Pipe.NE,
        'J' => Pipe.NW,
        '7' => Pipe.SW,
        'F' => Pipe.SE,
        '.' => Pipe.Ground,
        'S' => Pipe.Start,
        _ => throw new InvalidEnumArgumentException(nameof(pipeStr))
    };
}