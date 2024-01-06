using Day4;

Console.WriteLine(Solution1());
return;

static IEnumerable<string> ReadLines() => File.ReadLines("../../../../puzzleinput/day4.txt");

static IEnumerable<Card> ParseLines() => ReadLines().Select(line => new Card(line));

static int Solution1() => ParseLines().Sum(card => card.Points);