class Hand(string name)
{
    private readonly List<Card> cards = [];
    public readonly string PlayerName = name;

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public int GetValue()
    {
        int value = 0;
        int aceCount = 0;

        foreach (var card in cards)
        {
            value += card.GetValue();
            if (card.CardRank == Card.Rank.Ace)
                aceCount++;
        }

        // Adjust for Aces
        while (value > 21 && aceCount > 0)
        {
            value -= 10;
            aceCount--;
        }

        return value;
    }

    public bool IsBust()
    {
        return GetValue() > 21;
    }

    public bool IsBlackjack()
    {
        return cards.Count == 2 && GetValue() == 21;
    }

    public string ShowFirstCard()
    {
        if (cards.Count > 0)
            return cards[0].ToString();
        return "No cards";
    }

    public override string ToString()
    {
        return string.Join(", ", cards);
    }

    public void Clear()
    {
        cards.Clear();
    }
}