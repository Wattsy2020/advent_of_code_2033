using Common;

namespace Day4;

public class Card
{
    private readonly HashSet<int> _winningNumbers;
    private readonly int[] _cardNumbers;

    /// <summary>
    /// Read numbers separated by one or more space characters
    /// </summary>
    private static IEnumerable<int> ReadNumbers(string numbers)
    {
        int? startIdx = null;
        foreach (var (i, c) in numbers.Enumerate())
        {
            switch (c, startNumber: startIdx)
            {
                case (' ', not null):
                    yield return int.Parse(numbers[startIdx.Value..i]);
                    startIdx = null;
                    break;
                case (_, null):
                    startIdx = i;
                    break;
            }
        }

        if (startIdx is not null)
            yield return int.Parse(numbers[startIdx.Value..]);
    }

    public Card(string cardDescription)
    {
        string[] cards = cardDescription.Split(": ")[1].Split(" | ");
        _winningNumbers = ReadNumbers(cards[0]).ToHashSet();
        _cardNumbers = ReadNumbers(cards[1]).ToArray();
    }

    public int NumWinningCards => _cardNumbers.Where(_winningNumbers.Contains).Count();

    public int Points => (NumWinningCards is var n and > 0) ? (int)Math.Pow(2, n - 1) : 0;

    public override string ToString() => $"{_winningNumbers.AsString()}\n{_cardNumbers.AsString()}\n";
}