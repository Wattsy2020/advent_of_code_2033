using Common;
using Day6;

var fileContents = File.ReadAllText(FileSystem.PuzzleInputPath(6));
var raceDocument = new RaceDocument(fileContents);
Console.WriteLine(raceDocument.Solution1());