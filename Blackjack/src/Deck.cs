// A simple implementation of a deck consisting of 1 - 8 standard 52-card decks.
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
        InitializeDeck(this.numberOfDecks);
        Shuffle();
    }

    // Initialize the deck with the specified number of standard 52-card decks in sorted order
    private void InitializeDeck(int numberOfDecks)
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

    // Shuffle the deck using the Fisher-Yates algorithm
    private void Shuffle()
    {
        int n = cards.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (cards[j], cards[i]) = (cards[i], cards[j]);
        }
    }

    // Reshuffle the deck when all cards have been dealt
    private void ReShuffle()
    {
        Commens.WriteLineToUser("Reshuffling the deck...");
        cards.Clear();
        InitializeDeck(numberOfDecks);
        Shuffle();
    }

    // Take a card out of the deck
    public Card DealCard()
    {
        if (cards.Count == 0)
            ReShuffle();

        var card = cards[0];
        cards.RemoveAt(0);
        return card;
    }
}