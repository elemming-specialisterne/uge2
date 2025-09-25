//Keep track of scores for multiple players against the dealer
abstract class Scoring(string[] playerNames)
{
    protected readonly string[] players = playerNames;
    protected readonly int[] playerScores = new int[playerNames.Length];
    protected readonly int[] dealerScores = new int[playerNames.Length];
    protected int totalGames = 0;
    protected bool isBetting = false;

    // Increment the score for a player or dealer
    public abstract bool IncrementPlayerScore(int playerIndex, bool won);

    // Display the current scores for all players against the dealer
    public abstract void DisplayScores();

    // Set the bet amount for a player
    public abstract int SetBet(int playerIndex, int amount);

    // Increment the total number of games played
    public void IncrementTotalGames()
    {
        totalGames++;
    }

    // Check if betting system is used
    public bool IsBetting()
    {
        return isBetting;
    }
    
    // Get the current score of a players
    public int GetPlayerScore(int playerIndex)
    {
        if (playerIndex >= 0 && playerIndex < playerScores.Length)
            return playerScores[playerIndex];
        return -1; // Invalid index
    }
}