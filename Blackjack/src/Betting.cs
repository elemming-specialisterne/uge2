class Betting : Scoring
{
    public Betting(string[] playerNames, int startingBet) : base(playerNames)
    {
        playerBets = new int[playerNames.Length];
        isBetting = true;
        for (int i = 0; i < playerScores.Length; i++)
            playerScores[i] = startingBet; // Starting chips for each player
    }
    private readonly int[] playerBets;
    public override bool IncrementPlayerScore(int playerIndex, bool won)
    {
        if (playerIndex >= 0 && playerIndex < playerScores.Length)
        {
            if (won)
            {
                playerScores[playerIndex] += playerBets[playerIndex];
                dealerScores[playerIndex] -= playerBets[playerIndex];
            }
            else
            {
                playerScores[playerIndex] -= playerBets[playerIndex];
                dealerScores[playerIndex] += playerBets[playerIndex];
                return true;
            }
        }
        return false;
    }

    public override int SetBet(int playerIndex, int amount)
    {
        if (playerIndex >= 0 && playerIndex < playerScores.Length && amount > 0 && amount <= playerScores[playerIndex])
        {
            playerBets[playerIndex] = amount;
            return -1;
        }
        return playerScores[playerIndex];
    }

    public override void DisplayScores()
    {
        Commens.WriteLineToUser("------Current Scores------");
        Commens.WriteLineToUser("Total games played: " + totalGames);
        for (int i = 0; i < players.Length; i++)
            if (playerScores[i] < 0)
                Commens.WriteLineToUser($"{players[i]} is out after losing {dealerScores[i]} Chips");
            else if (dealerScores[i] > 0)
                Commens.WriteLineToUser($"{players[i]}: {playerScores[i]} Chips, Losing {dealerScores[i]} Chips");
            else
                Commens.WriteLineToUser($"{players[i]}: {playerScores[i]} Chips, Winning {-dealerScores[i]} Chips");
        Commens.WriteLineToUser("Total dealer winnings: " + dealerScores.Sum());
    }
}