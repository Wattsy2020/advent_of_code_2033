namespace Common;

public static class FileSystem
{
    private static readonly string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    private static readonly string PuzzleInputDirectory =
        Path.Combine(UserProfile, "code/advent_of_code_2033/puzzleinput");

    public static string PuzzleInputPath(int dayNumber) => Path.Combine(PuzzleInputDirectory, $"day{dayNumber}.txt");
}