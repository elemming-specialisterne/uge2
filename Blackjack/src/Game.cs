using System.Globalization;

class Game
{
    private readonly Deck deck;
    private readonly List<Hand> playerHands;
    private readonly List<Hand> BotHands;

    public Game(string[] playerNames, string[] botNames, int numberOfDecks)
    {
        deck = new(numberOfDecks);
        playerHands = [];
        for (int i = 0; i < playerNames.Length; i++)
            playerHands.Add(new(playerNames[i]));
        BotHands = [];
        for (int i = 0; i < botNames.Length; i++)
            BotHands.Add(new(botNames[i]));
        // Always add one dealer
        BotHands.Add(new("The Dealer"));
    }
    public void Start()
    {
        Commens.WriteLineToUser("Starting a new round...");
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
        Commens.WriteLineToUser(playerHand.PlayerName + "'s open card: " + playerHand.ShowSecondCard());
    }

    public void PlayerTurn(Hand playerHand)
    {
        while (true)
        {
            //Check if player is bust or blackjack
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

            // Ask player to Hit or Stand
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
        // check for blackjack
        if (BotHand.IsBlackjack())
        {
            Commens.WriteLineToUser($"{BotHand.PlayerName} has Blackjack!");
            return;
        }

        Commens.WriteLineToUser($"{BotHand.PlayerName}'s hand: {BotHand} (Value: {BotHand.GetValue()})");
        while (BotHand.GetValue() < 17)
        {
            Commens.WriteLineToUser($"{BotHand.PlayerName} hits.");
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