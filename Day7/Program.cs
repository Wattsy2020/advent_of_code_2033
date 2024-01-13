using Common;
using Day7;

var problemInput = File.ReadAllText(FileSystem.PuzzleInputPath(7));
var handCollection = new HandCollection(problemInput);
Console.WriteLine(handCollection.Solution1());