namespace Common;

public static class Parse
{
    /// <summary>
    /// Read numbers separated by one or more space characters
    /// </summary>
    public static IEnumerable<int> ReadNumbers(string numbers)
    {
        int? startIdx = null;
        foreach (var (i, c) in numbers.Enumerate())
        {
            var isDigit = char.IsDigit(c);
            if (isDigit && startIdx is null)
            {
                startIdx = i;
            }
            else if (!isDigit && startIdx is not null)
            {
                yield return int.Parse(numbers[startIdx.Value..i]);
                startIdx = null;
            }
        }

        if (startIdx is not null)
            yield return int.Parse(numbers[startIdx.Value..]);
    }
}