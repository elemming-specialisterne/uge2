class Deck
{
    private readonly List<Card> cards;
    private readonly Random random;
    private readonly int numberOfDecks;

    public Deck(int numberOfDecks = 1)
    {
        cards = [];
        random = new Random();
        this.numberOfDecks = numberOfDecks;
        InitializeDeck();
        Shuffle();
    }

    private void InitializeDeck()
    {
        Card.Suit[] suits = Enum.GetValues<Card.Suit>();
        Card.Rank[] ranks = Enum.GetValues<Card.Rank>();
        
        for (int d = 0; d < numberOfDecks; d++)
            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    cards.Add(new Card(suit, rank));
                }
            }
    }

    private void Shuffle()
    {
        int n = cards.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (cards[j], cards[i]) = (cards[i], cards[j]);
        }
    }

    private void ReShuffle()
    {
        Commens.WriteLineToUser("Reshuffling the deck...");
        cards.Clear();
        InitializeDeck();
        Shuffle();
    }

    public Card DealCard()
    {
        if (cards.Count == 0)
            ReShuffle();

        var card = cards[0];
        cards.RemoveAt(0);
        return card;
    }
}