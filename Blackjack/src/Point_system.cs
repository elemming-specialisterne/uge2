class Point_system : Scoring
{
    public Point_system(string[] playerNames) : base(playerNames)
    {
        isBetting = false;
        for (int i = 0; i < playerScores.Length; i++)
            playerScores[i] = 0; // Starting points for each player
    }
    // Increment the score for a player or dealer
    public override bool IncrementPlayerScore(int playerIndex, bool won)
    {
        if (playerIndex >= 0 && playerIndex < playerScores.Length)
        {
            if (won)
                playerScores[playerIndex]++;
            else
                dealerScores[playerIndex]++;
        }
        return false;
    }

    // Display the current scores for all players against the dealer
    public override void DisplayScores()
    {
        Commens.WriteLineToUser("------Current Scores------");
        Commens.WriteLineToUser("Total games played: " + totalGames);
        for (int i = 0; i < players.Length; i++)
            Commens.WriteLineToUser($"{players[i]}: {playerScores[i]} wins, Dealer: {dealerScores[i]} wins");
        Commens.WriteLineToUser("Total dealer wins: " + dealerScores.Sum());
    }

    // No betting in point system
    public override int SetBet(int playerIndex, int amount)
    {
        return 0;
    }
}