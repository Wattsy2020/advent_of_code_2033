Console.WriteLine(Solution());

static IEnumerable<string> ReadInput() => File.ReadLines("../puzzleinput/day1.txt");

static int CalculateCalibration(string line) => int.Parse($"{line.First(char.IsDigit)}{line.Last(char.IsDigit)}");

static int Solution() => ReadInput().Select(CalculateCalibration).Sum();