using Common;
using Day2;

var bagContents = new CubeCollection(12, 14, 13);
Console.WriteLine(Part1Solution());
Console.WriteLine(Part2Solution());
return;

static IEnumerable<string> ParseLines() => File.ReadLines(FileSystem.PuzzleInputPath(2));

static IEnumerable<CubeGame> ParseInput(IEnumerable<string> lines) => lines.Select(line => new CubeGame(line));

int Part1Solution() =>
    ParseInput(ParseLines())
        .Where(game => game.IsPossible(bagContents))
        .Sum(game => game.Id);

int Part2Solution() =>
    ParseInput(ParseLines())
        .Select(game => game.MinimumPossibleCollection())
        .Sum(game => game.Power);