using System.Globalization;

class Game
{
    private readonly Deck deck;
    private readonly List<Hand> playerHands;
    private readonly Scoring scoring;

    public Game(string[] names, bool[] isBot, int numberOfDecks, Scoring scoring)
    {
        this.scoring = scoring;
        deck = new(numberOfDecks);
        playerHands = [];
        for (int i = 0; i < names.Length; i++)
            playerHands.Add(new(names[i], isBot[i]));
        // Always add one dealer
        playerHands.Add(new("The Dealer", true));
    }
    public void Start()
    {
        Commens.WriteLineToUser("Starting a new round...");
        Commens.WriteLineToUser("Dealing cards...");

        // Initial dealling
        foreach (var playerHand in playerHands)
            DealInitialCards(playerHand);

        // Player turns
        foreach (var playerHand in playerHands)
            if (playerHand.IsBot())
                BotTurn(playerHand);
            else
                PlayerTurn(playerHand);

        // Determine results
        foreach (var hand in playerHands[..^1]) // Exclude dealer
        {
            var result = DetermineWinner(hand, playerHands[^1]); // Compare with dealer
            Commens.WriteLineToUser(result);
        }

        ClearHands();

        scoring.DisplayScores();
        Commens.WriteLineToUser("Round over. Press Enter to continue...");
        Commens.WaitForUser();
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

    private string DetermineWinner(Hand playerHand, Hand dealerHand)
    {
        if (playerHand.IsBlackjack())
        {
            scoring.IncrementPlayerScore(playerHands.IndexOf(playerHand), true);
            return $"{playerHand.PlayerName} has Blackjack and wins against {dealerHand.PlayerName}!";
        }
        if (dealerHand.IsBust())
        {
            scoring.IncrementPlayerScore(playerHands.IndexOf(playerHand), true);
            return $"{dealerHand.PlayerName} busts. {playerHand.PlayerName} wins!";
        }
        if (playerHand.IsBust())
        {
            scoring.IncrementPlayerScore(playerHands.IndexOf(playerHand), false);
            return $"{playerHand.PlayerName} busts and loses to {dealerHand.PlayerName}.";
        }
        if (dealerHand.IsBlackjack())
        {
            scoring.IncrementPlayerScore(playerHands.IndexOf(playerHand), false);
            return $"{dealerHand.PlayerName} has Blackjack and wins against {playerHand.PlayerName}.";
        }

        int playerValue = playerHand.GetValue();
        int dealerValue = dealerHand.GetValue();
        if (playerValue > dealerValue)
        {
            scoring.IncrementPlayerScore(playerHands.IndexOf(playerHand), true);
            return $"{playerHand.PlayerName} wins against {dealerHand.PlayerName}!";
        }
        if (playerValue < dealerValue)
        {
            scoring.IncrementPlayerScore(playerHands.IndexOf(playerHand), false);
            return $"{playerHand.PlayerName} loses to {dealerHand.PlayerName}.";
        }
        return $"{playerHand.PlayerName} has a push with {dealerHand.PlayerName}.";
    }

    private void ClearHands()
    {
        foreach (var hand in playerHands)
            hand.Clear();
    }
}