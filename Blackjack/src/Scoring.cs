//Keep track of scores for multiple players against the dealer
class Scoring(string[] playerNames)
{
    private readonly string[] players = playerNames;
    private readonly int[] playerScores = new int[playerNames.Length];
    private readonly int[] dealerScores = new int[playerNames.Length];
    private int totalGames = 0;

    // Increment the score for a player or dealer
    public void IncrementPlayerScore(int playerIndex, bool won)
    {
        if (playerIndex >= 0 && playerIndex < playerScores.Length)
        {
            if (won)
                playerScores[playerIndex]++;
            else
                dealerScores[playerIndex]++;
        }
    }

    // Increment the total number of games played
    public void IncrementTotalGames()
    {
        totalGames++;
    }

    // Display the current scores for all players against the dealer
    public void DisplayScores()
    {
        Commens.WriteLineToUser("------Current Scores------");
        Commens.WriteLineToUser("Total games played: " + totalGames);
        for (int i = 0; i < players.Length; i++)
            Commens.WriteLineToUser($"{players[i]}: {playerScores[i]} wins, Dealer: {dealerScores[i]} wins");
        Commens.WriteLineToUser("Total dealer wins: " + dealerScores.Sum());
    }
}