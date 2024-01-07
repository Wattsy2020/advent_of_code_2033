using Common;

Dictionary<string, int> digitMapping = new()
{
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 },
};
const int minDigitLength = 3;
const int maxDigitLength = 5;
Console.WriteLine(Solution());
return;

static IEnumerable<string> ReadInput() => File.ReadLines(FileSystem.PuzzleInputPath(1));

static int ConcatDigits(int digit1, int digit2) => digit1 * 10 + digit2;

int? FindDigit(string line, int index)
{
    if (int.TryParse(line[index..(index + 1)], out int result))
        return result;
    int maxLength = Math.Min(maxDigitLength, line.Length - index);
    for (int i = minDigitLength; i <= maxLength; i++)
        if (digitMapping.TryGetValue(line[index .. (index + i)], out result))
            return result;
    return null;
}

int FirstDigit(string line, IEnumerable<int> indexes)
    => indexes.Select(idx => FindDigit(line, idx)).First(d => d.HasValue)!.Value;

int CalculateCalibration(string line)
{
    var firstDigit = FirstDigit(line, Enumerable.Range(0, line.Length));
    var lastDigit = FirstDigit(line, Enumerable.Range(0, line.Length).Reverse());
    return ConcatDigits(firstDigit, lastDigit);
}

int Solution() => ReadInput().Select(CalculateCalibration).Sum();