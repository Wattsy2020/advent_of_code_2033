using Day9;

var puzzleInputPath = Path.Combine(AppContext.BaseDirectory, $"day9.txt");
var contents = File.ReadLines(puzzleInputPath);
var report = new OasisReport(contents);
Console.WriteLine(report.Solution1());
Console.WriteLine(report.Solution2());