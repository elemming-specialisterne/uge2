// A simple representation of a playing card
class Card(Card.Suit suit, Card.Rank rank)
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,
        Queen = 10,
        King = 10,
        Ace = 11 // Initially consider Ace as 11
    }

    public Suit CardSuit { get; private set; } = suit;
    public Rank CardRank { get; private set; } = rank;

    // Get the value of the Card's CardRank
    public int GetValue()
    {
        return (int)CardRank;
    }

    // Have ToString display readable card info
    public override string ToString()
    {
        return $"{CardRank} of {CardSuit}";
    }
}