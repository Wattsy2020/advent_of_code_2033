using Common;
using Day5;

string fileContents = File.ReadAllText(FileSystem.PuzzleInputPath(5));
var almanac = new Almanac(fileContents);
Console.WriteLine(almanac.Solution2());