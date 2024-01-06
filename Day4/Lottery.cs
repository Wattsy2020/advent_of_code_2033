using Common;

namespace Day4;

public class Lottery(IEnumerable<Card> cards)
{
    private IEnumerable<Card> _cards = cards;

    public int CalcPoints()
    {
        // map card number (starting from 0), to the number of duplicates of those cards 
        // including the original card
        Dictionary<int, int> cardCounts = new();

        foreach (var (i, card) in _cards.Enumerate())
        {
            var cardCount = (cardCounts.OptGetValue(i) ?? 0) + 1;
            cardCounts[i] = cardCount;
            foreach (var j in Enumerable.Range(1, card.NumWinningCards))
            {
                cardCounts[i + j] = (cardCounts.OptGetValue(i + j) ?? 0) + cardCount;
            }
        }

        return cardCounts.Values.Sum();
    }
}