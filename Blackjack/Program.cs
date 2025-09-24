class Program
{
    private int numberOfDecks = 1;
    private string[] playerNames = ["Player1"];
    private bool[] isBot = [];

    public Program()
    {
        Commens.WriteLineToUser("Welcome to Blackjack!");
        Commens.WriteLineToUser("This is a console-based Blackjack game.");

        Commens.WriteLineToUser("Press Enter to start the game...");
        Commens.WaitForUser();
    }

    private void Run()
    {
        Commens.ClearConsole();
        SetupGame();

        Game game = new(playerNames, isBot, numberOfDecks, new Scoring(playerNames));
        while (true)
        {
            Commens.ClearConsole();
            game.Start();


            string again = Commens.GetYorN("Do you want to play again? (Y/N): ");
            if (again == "N")
                break;
            if (again == "Y")
            {
                string newDeck = Commens.GetYorN("Do you want a new Deck? (Y/N): ");
                if (newDeck == "Y")
                {
                    string newSetup = Commens.GetYorN("Do you want a new setup? (Y/N): ");
                    if (newSetup == "Y")
                    {
                        SetupGame();
                    }
                    game = new(playerNames, isBot, numberOfDecks, new Scoring(playerNames));
                }
            }
            Commens.ClearConsole();

        }

        Commens.WriteLineToUser("Thanks for playing! Goodbye!");
    }

    private void SetupGame()
    {
        int numberOfPlayers = Commens.GetIntFromUser("Enter number of players (1-4): ", 1, 4);
        playerNames = new string[numberOfPlayers];
        isBot = new bool[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++)
        {
            playerNames[i] = Commens.GetStringFromUser($"Player {i + 1}, please enter name:");
            isBot[i] = Commens.GetYorN($"Is {playerNames[i]} a bot? (Y/N): ") == "Y";
            Commens.WriteLineToUser($"Welcome, {playerNames[i]}!");
        }
        numberOfDecks = Commens.GetIntFromUser("Enter number of decks (1-8): ", 1, 8);
    }

    static void Main()
    {
        var Program = new Program();
        Program.Run();
    }
        
}



