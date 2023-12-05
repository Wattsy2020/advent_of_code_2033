using Day2;

var bagContents = new CubeCollection(12, 14, 13);
Console.WriteLine(Solution());
return;

static IEnumerable<string> ParseLines() => File.ReadLines("../puzzleinput/day2.txt");

static IEnumerable<CubeGame> ParseInput(IEnumerable<string> lines) => lines.Select(line => new CubeGame(line));

int Solution() =>
    ParseInput(ParseLines())
        .Where(game => game.IsPossible(bagContents))
        .Sum(game => game.Id);
 