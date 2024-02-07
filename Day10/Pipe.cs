using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Day10;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum Pipe
{
    /// <summary>
    /// | is a vertical pipe connecting north and south.
    /// </summary>
    NS = 0,

    /// <summary>
    /// - is a horizontal pipe connecting east and west.
    /// </summary>
    EW = 1,

    /// <summary>
    /// L is a 90-degree bend connecting north and east.
    /// </summary>
    NE = 2,

    /// <summary>
    /// J is a 90-degree bend connecting north and west.
    /// </summary>
    NW = 3,

    /// <summary>
    /// 7 is a 90-degree bend connecting south and west.
    /// </summary>
    SW = 4,

    /// <summary>
    /// F is a 90-degree bend connecting south and east.
    /// </summary>
    SE = 5,

    /// <summary>
    /// . is ground; there is no pipe in this tile
    /// </summary>
    Ground = 6,

    /// <summary>
    /// S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has
    /// </summary>
    Start = 7,
}

public static class PipeUtils
{
    private static readonly HashSet<Pipe> NorthConnectingPipes = new() { Pipe.NE, Pipe.NW, Pipe.NS };
    private static readonly HashSet<Pipe> SouthConnectingPipes = new() { Pipe.SE, Pipe.SW, Pipe.NS };
    private static readonly HashSet<Pipe> EastConnectingPipes = new() { Pipe.NE, Pipe.SE, Pipe.EW };
    private static readonly HashSet<Pipe> WestConnectingPipes = new() { Pipe.NW, Pipe.SW, Pipe.EW };

    public static bool IsNorthConnecting(Pipe pipe) => NorthConnectingPipes.Contains(pipe);

    public static bool IsSouthConnecting(Pipe pipe) => SouthConnectingPipes.Contains(pipe);

    public static bool IsEastConnecting(Pipe pipe) => EastConnectingPipes.Contains(pipe);

    public static bool IsWestConnecting(Pipe pipe) => WestConnectingPipes.Contains(pipe);

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