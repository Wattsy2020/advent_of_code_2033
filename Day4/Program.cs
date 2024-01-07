using Common;
using Day4;

Console.WriteLine(Solution1());
Console.WriteLine(Solution2());
return;

static IEnumerable<string> ReadLines() => File.ReadLines(FileSystem.PuzzleInputPath(4));

static IEnumerable<Card> ParseLines() => ReadLines().Select(line => new Card(line));

static int Solution1() => ParseLines().Sum(card => card.Points);

static int Solution2() => new Lottery(ParseLines()).CalcPoints();