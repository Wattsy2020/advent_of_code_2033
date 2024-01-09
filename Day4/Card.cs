using Common;

namespace Day4;

public class Card
{
    private readonly HashSet<int> _winningNumbers;
    private readonly int[] _cardNumbers;

    public Card(string cardDescription)
    {
        string[] cards = cardDescription.Split(": ")[1].Split(" | ");
        _winningNumbers = Parse.ReadNumbers(cards[0]).ToHashSet();
        _cardNumbers = Parse.ReadNumbers(cards[1]).ToArray();
    }

    public int NumWinningCards => _cardNumbers.Where(_winningNumbers.Contains).Count();

    public int Points => (NumWinningCards is var n and > 0) ? (int)Math.Pow(2, n - 1) : 0;

    public override string ToString() => $"{_winningNumbers.AsString()}\n{_cardNumbers.AsString()}\n";
}