using Common;
using Day8;

var problemInput = File.ReadAllText(FileSystem.PuzzleInputPath(8));
var map = new Map(problemInput);
Console.WriteLine(map.Solution1());