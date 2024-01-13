namespace Day7;

public class Hand
{
    /// <summary>
    /// The types of hands
    /// with their integer values representing their rank relative to each other 
    /// </summary>
    private enum HandType
    {
        FiveOfAKind = 6,
        FourOfAKind = 5,
        FullHouse = 4,
        ThreeOfAKind = 3,
        TwoPairs = 2,
        OnePair = 1,
        HighCard = 0,
    }

    private class Card
    {
        public readonly char Name;
        public readonly int Value;

        public Card(char name)
        {
            Name = name;
            Value = CardValue(Name);
        }

        private static int CardValue(char name) => name switch
        {
            _ when char.IsDigit(name) => int.Parse(name.ToString()),
            'T' => 10,
            'J' => 11,
            'Q' => 12,
            'K' => 13,
            'A' => 14,
            _ => throw new ArgumentException($"Cannot convert {name} to card")
        };
    }

    private readonly HandType _type;
    private readonly Card[] _cards;

    /// <summary>
    /// Initialise a hand given a string of the 5 cards it contains
    /// </summary>
    public Hand(string contents)
    {
        _cards = contents.Select(c => new Card(c)).ToArray();
        _type = CalcHandType(_cards);
    }

    private static HandType CalcHandType(Card[] cards)
    {
        var cardCounts = cards
            .GroupBy(card => card.Value)
            .Select(group => group.Count())
            .OrderDescending()
            .ToArray();
        if (cardCounts[0] == 5)
            return HandType.FiveOfAKind;
        if (cardCounts[0] == 4)
            return HandType.FourOfAKind;
        if (cardCounts[0] == 3 && cardCounts[1] == 2)
            return HandType.FullHouse;
        if (cardCounts[0] == 3)
            return HandType.ThreeOfAKind;
        if (cardCounts[0] == 2 && cardCounts[1] == 2)
            return HandType.TwoPairs;
        if (cardCounts[0] == 2)
            return HandType.OnePair;
        return HandType.HighCard;
    }

    public override string ToString() => $"{_type} {new string(_cards.Select(card => card.Name).ToArray())}";

    // TODO: logic for comparing hands
}