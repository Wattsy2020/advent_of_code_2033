using Day9;

var contents = File.ReadLines(FileSystem.PuzzleInputPath(9));
var report = new OasisReport(contents);
Console.WriteLine(report.Solution1());

// copy paste while developing on .NET6 machine
internal static class FileSystem
{
    // the build process copies the puzzle inputs here
    private static readonly string PuzzleInputDirectory = AppContext.BaseDirectory;

    public static string PuzzleInputPath(int dayNumber) => Path.Combine(PuzzleInputDirectory, $"day{dayNumber}.txt");
}