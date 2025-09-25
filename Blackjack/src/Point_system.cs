class Point_system(string[] playerNames) : Scoring(playerNames)
{

    // Increment the score for a player or dealer
    public override void IncrementPlayerScore(int playerIndex, bool won)
    {
        if (playerIndex >= 0 && playerIndex < playerScores.Length)
        {
            if (won)
                playerScores[playerIndex]++;
            else
                dealerScores[playerIndex]++;
        }
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
}