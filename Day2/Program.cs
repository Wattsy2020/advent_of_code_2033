using Day2;

CubeCollection bagContents = new CubeCollection("12 red, 13 green, 14 blue");
Console.WriteLine(Solution());
return;

static IEnumerable<string> ParseLines() => File.ReadLines("../puzzleinput/day2.txt");

static IEnumerable<CubeGame> ParseInput(IEnumerable<string> lines) => lines.Select(line => new CubeGame(line));

int Solution() =>
    ParseInput(ParseLines())
        .Where(game => game.IsPossible(bagContents))
        .Sum(game => game.id);
 