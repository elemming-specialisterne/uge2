using System.Globalization;

class Game
{
    private readonly Deck deck;
    private readonly List<Player> players;
    private readonly Scoring scoring;

    public Game(string[] names, bool[] isBot, int numberOfDecks, Scoring scoring)
    {
        this.scoring = scoring;
        deck = new(numberOfDecks);
        players = [];
        for (int i = 0; i < names.Length; i++)
            players.Add(new(names[i], isBot[i]));
        // Always add one dealer
        players.Add(new("The Dealer", true));
    }
    public void Start()
    {
        Commens.WriteLineToUser("Starting a new round...");

        if (scoring.IsBetting())
        {
            // Get bets from players
            Commens.WriteLineToUser("Placing bets...");
            for (int i = 0; i < players.Count - 1; i++) // Exclude dealer
            {
                int currentScore = scoring.GetPlayerScore(i);
                if (players[i].IsBot())
                {
                    // Simple bot logic: 10% of chips or 10 or all remaining if less than 10
                    int botBet = Math.Min(Math.Max(10, Convert.ToInt32(currentScore*0.1)), currentScore);
                    scoring.SetBet(i, botBet);
                    Commens.WriteLineToUser($"{players[i].GetName()} bets {botBet} Chips");
                    continue;
                }
                Commens.WriteLineToUser($"{players[i].GetName()}'s current score: {currentScore} Chips");
                int bet;
                while (true)
                {
                    if (players[i].IsOut())
                    {
                        Commens.WriteLineToUser($"{players[i].GetName()} is out of the game!");
                        break;
                    }
                    if (currentScore <= 0)
                    {
                        Commens.WriteLineToUser($"{players[i].GetName()} has no Chips left to bet and is out!");
                        players[i].SetOut(true);
                        break;
                    }
                    bet = Commens.GetIntFromUser($"{players[i].GetName()}, enter your bet amount (max {currentScore}): ", 1, currentScore);
                    int remainingChips = scoring.SetBet(i, bet);
                    if (remainingChips == -1)
                        break; // Bet accepted
                    else
                        Commens.WriteLineToUser($"Invalid bet. You have {remainingChips} Chips remaining.");
                }
            }
        }


        // Initial dealling
        Commens.WriteLineToUser("Dealing cards...");
        foreach (var playerHands in players)
            foreach (var playerHand in playerHands.GetHands())
                DealInitialCards(playerHand);

        Commens.WriteLineToUser(players[^1].GetName() + "'s open card: " + players[^1].GetHands()[0].ShowSecondCard());

        // Player turns
        foreach (var player in players)
        {
            if (player.IsBot())
                BotTurn(player);
            else
                PlayerTurn(player);
            Commens.WaitForUser();
        }

        // Determine results
            string result;
        foreach (var player in players[..^1]) // Exclude dealer
        {
            if (player.GetHands().Count > 1)
            {
                for (int i = 0; i < player.GetHands().Count; i++)
                {
                    result = DetermineWinner(player, i, players[^1]); // Compare with dealer
                    Commens.WriteLineToUser(result);
                }
            }
            else
            {
                result = DetermineWinner(player, 0, players[^1]); // Compare with dealer
                Commens.WriteLineToUser(result);
            }
        }

        ClearHands();

        scoring.IncrementTotalGames();
        scoring.DisplayScores();
        Commens.WriteLineToUser("Round over. Press Enter to continue...");
        Commens.WaitForUser();
    }

    public void DealInitialCards(Hand playerHand)
    {
        playerHand.AddCard(deck.DealCard());
        playerHand.AddCard(deck.DealCard());
    }
    
    public void PlayerTurn(Player player)
    {
        for (int i = 0;  i < player.GetHands().Count; i++)
        {
            Hand hand = player.GetHands()[i];
            PlayerHandTurn(hand, player, i);
        }
    }

    public void PlayerHandTurn(Hand playerHand, Player player, int handnumber)
    {
        while (true)
        {
            //Check if player is bust or blackjack
            if (player.GetHands().Count > 1)
                Commens.WriteLineToUser($"\n{player.GetName()}'s turn for hand {handnumber + 1}: {playerHand} (Value: {playerHand.GetValue()})");
            else
                Commens.WriteLineToUser($"\n{player.GetName()}'s turn: {playerHand} (Value: {playerHand.GetValue()})");
            if (playerHand.CanSplit())
            {
                var splitChoice = Commens.GetYorN("Do you want to split? (Y/N): ");
                if (splitChoice == "Y")
                {
                    Commens.WriteLineToUser("Splitting hand...");
                    var newHand = new Hand(playerHand.IsBot());
                    newHand.AddCard(playerHand.RemoveSecondCard());
                    player.AddHand(newHand);
                    playerHand.AddCard(deck.DealCard());
                    newHand.AddCard(deck.DealCard());
                    continue; // Continue with the original hand
                }
            }
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

    private void BotTurn(Player bot)
    {
        foreach (var hand in bot.GetHands())
        {
            Commens.WriteLineToUser($"\n{bot.GetName()}'s turn for hand: {hand} (Value: {hand.GetValue()})");
            BotHandTurn(hand, bot.GetName());
        }
    }

    private void BotHandTurn(Hand botHand, string botName)
    {
        // check for blackjack
        if (botHand.IsBlackjack())
        {
            Commens.WriteLineToUser($"{botName} has Blackjack!");
            return;
        }

        Commens.WriteLineToUser($"{botName}'s hand: {botHand} (Value: {botHand.GetValue()})");
        while (botHand.GetValue() < 17)
        {
            Commens.WriteLineToUser($"{botName} hits.");
            botHand.AddCard(deck.DealCard());
            Commens.WriteLineToUser($"{botName}'s hand: {botHand} (Value: {botHand.GetValue()})");
        }

        if (botHand.IsBust())
            Commens.WriteLineToUser($"{botName} busts!");
        else
            Commens.WriteLineToUser($"{botName} stands with value {botHand.GetValue()}");
    }

    private string DetermineWinner(Player player, int handNumber, Player dealer)
    {
        string playerName = player.GetHands().Count > 1 ? $"{player.GetName()}'s {handNumber+1}.Hand" : player.GetName();
        var playerHand = player.GetHands()[handNumber];
        if (playerHand.IsBlackjack())
        {
            scoring.IncrementPlayerScore(players.IndexOf(player), true);
            return $"{playerName} has Blackjack and wins against {dealer.GetName()}!";
        }
        if (dealer.GetHands()[0].IsBust())
        {
            scoring.IncrementPlayerScore(players.IndexOf(player), true);
            return $"{dealer.GetName} busts. {playerName} wins!";
        }
        if (playerHand.IsBust())
        {
            scoring.IncrementPlayerScore(players.IndexOf(player), false);
            return $"{playerName} busts and loses to {dealer.GetName()}.";
        }
        if (dealer.GetHands()[0].IsBlackjack())
        {
            scoring.IncrementPlayerScore(players.IndexOf(player), false);
            return $"{dealer.GetName()} has Blackjack and wins against {playerName}.";
        }

        int playerValue = playerHand.GetValue();
        int dealerValue = dealer.GetHands()[0].GetValue();
        if (playerValue > dealerValue)
        {
            scoring.IncrementPlayerScore(players.IndexOf(player), true);
            return $"{playerName} wins against {dealer.GetName()}!";
        }
        if (playerValue < dealerValue)
        {
            scoring.IncrementPlayerScore(players.IndexOf(player), false);
            return $"{playerName} loses to {dealer.GetName()}.";
        }
        return $"{playerName} has a push with {dealer.GetName()}.";
    }

    private void ClearHands()
    {
        foreach (var player in players)
            player.Clear();
    }
}