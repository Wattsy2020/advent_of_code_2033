using Common;
using Day5;

string fileContents = File.ReadAllText("../../../../puzzleinput/day5.txt");
var almanac = new Almanac(fileContents);
Console.WriteLine(almanac.Destinations.AsString());
Console.WriteLine(almanac.Solution1());