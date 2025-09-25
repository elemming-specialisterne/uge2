//Keep track of scores for multiple players against the dealer
abstract class Scoring(string[] playerNames)
{
    protected readonly string[] players = playerNames;
    protected readonly int[] playerScores = new int[playerNames.Length];
    protected readonly int[] dealerScores = new int[playerNames.Length];
    protected int totalGames = 0;

    // Increment the score for a player or dealer
    public abstract void IncrementPlayerScore(int playerIndex, bool won);

    // Increment the total number of games played
    public void IncrementTotalGames()
    {
        totalGames++;
    }

    // Display the current scores for all players against the dealer
    public abstract void DisplayScores();
}