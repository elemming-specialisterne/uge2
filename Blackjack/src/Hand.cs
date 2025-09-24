class Hand(string name)
{
    private readonly List<Card> cards = [];
    public readonly string PlayerName = name;

    // Add a card to the hand
    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    // Calculate the value of the hand, considering Aces as 1 or 11
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

    // Check if the hand is bust (over 21)
    public bool IsBust()
    {
        return GetValue() > 21;
    }

    // Check if the hand is a blackjack (exactly 21 with two cards)
    public bool IsBlackjack()
    {
        return cards.Count == 2 && GetValue() == 21;
    }

    // Show the Second card in the hand
    public string ShowSecondCard()
    {
        if (cards.Count > 1)
            return cards[1].ToString();
        return "No secound card";
    }

    // Show all cards in the hand
    public override string ToString()
    {
        return string.Join(", ", cards);
    }

    // Clear the hand of Cards for a new round
    public void Clear()
    {
        cards.Clear();
    }
}