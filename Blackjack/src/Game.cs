using System.Globalization;

class Game
{
    private readonly Deck deck;
    private readonly List<Hand> playerHands;
    private readonly List<Hand> BotHands;

    public Game(int numberOfPLayers = 1, int numberOfBots = 0, int numberOfDecks = 1)
    {
        deck = new(numberOfDecks);
        playerHands = [];
        for (int i = 0; i < numberOfPLayers; i++)
            playerHands.Add(new("Player " + (i + 1)));
        BotHands = [];
        for (int i = 0; i < numberOfBots; i++)
            BotHands.Add(new("Bot " + (i + 1)));
        // Always add one dealer
        BotHands.Add(new("The Dealer"));
    }
    public void Start()
    {
        Commens.WriteLineToUser("Blackjack started!");
        Commens.WriteLineToUser("Dealing cards...");

        // Initial dealling
        foreach (var playerHand in playerHands)
            DealInitialCards(playerHand);
        foreach (var botHand in BotHands)
            DealInitialCards(botHand);

        // Player turns
        foreach (var playerHand in playerHands)
            PlayerTurn(playerHand);
        foreach (var botHand in BotHands)
            BotTurn(botHand);

        // Determine results
        foreach (var playerHand in playerHands)
        {
            var result = DetermineWinner(playerHand, BotHands[^1]); // Compare with dealer
            Commens.WriteLineToUser(result);
        }

        ClearHands();
    }

    public void DealInitialCards(Hand playerHand)
    {
        playerHand.AddCard(deck.DealCard());
        playerHand.AddCard(deck.DealCard());
        Commens.WriteLineToUser(playerHand.PlayerName + "'s first card: " + playerHand.ShowFirstCard());
    }

    public void PlayerTurn(Hand playerHand)
    {
        while (true)
        {
            Commens.WriteLineToUser($"{playerHand.PlayerName}'s hand: {playerHand} (Value: {playerHand.GetValue()})");
            if (playerHand.IsBlackjack())
            {
                Commens.WriteLineToUser("Blackjack!");
                return;
            }
            else if (playerHand.IsBust())
            {
                Commens.WriteLineToUser("Bust!");
                return;
            }

            var choice = Commens.GetHorS("Do you want to Hit or Stand? (H/S): ");
            if (choice == "H")
            {
                Commens.WriteLineToUser("Hitting...");
                playerHand.AddCard(deck.DealCard());
            }
            else if (choice == "S")
            {
                Commens.WriteLineToUser($"Standing with value {playerHand.GetValue()}");
                break;
            }
        }
    }

    private void BotTurn(Hand BotHand)
    {
        Commens.WriteLineToUser($"{BotHand.PlayerName}'s hand: {BotHand} (Value: {BotHand.GetValue()})");
        while (BotHand.GetValue() < 17)
        {
            Commens.WriteLineToUser("Dealer hits.");
            BotHand.AddCard(deck.DealCard());
            Commens.WriteLineToUser($"{BotHand.PlayerName}'s hand: {BotHand} (Value: {BotHand.GetValue()})");
        }
        if (BotHand.IsBust())
            Commens.WriteLineToUser($"{BotHand.PlayerName} busts!");
        else
            Commens.WriteLineToUser($"{BotHand.PlayerName} stands with value {BotHand.GetValue()}");
    }

    private string DetermineWinner(Hand playerHand, Hand BotHand)
    {
        
        if (playerHand.IsBust())
            return $"{playerHand.PlayerName} busts and loses to {BotHand.PlayerName}.";
        if (playerHand.IsBlackjack())
            return $"{playerHand.PlayerName} has Blackjack and wins against {BotHand.PlayerName}!";
        if (BotHand.IsBust())
            return $"{BotHand.PlayerName} busts. {playerHand.PlayerName} wins!";
        int playerValue = playerHand.GetValue();
        int dealerValue = BotHand.GetValue();
        if (playerValue > dealerValue)
            return $"{playerHand.PlayerName} wins against {BotHand.PlayerName}!";
        if (playerValue < dealerValue)
            return $"{playerHand.PlayerName} loses to {BotHand.PlayerName}.";
        return $"{playerHand.PlayerName} has a push with {BotHand.PlayerName}.";
    }

    private void ClearHands()
    {
        foreach (var hand in playerHands)
            hand.Clear();
        foreach (var hand in BotHands)
            hand.Clear();
    }
}