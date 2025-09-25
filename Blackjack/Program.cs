class Program
{
    private int numberOfDecks = 1;
    private string[] playerNames = ["Player1"];
    private bool[] isBot = [];
    private Scoring? scoringChoice;
    private Game? game;

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

        while (true)
        {
            Commens.ClearConsole();
            game?.Start();


            string again = Commens.GetYorN("Do you want to play again? (Y/N): ");
            if (again == "N")
            {
                string newDeck = Commens.GetYorN("Do you want a new Deck? (Y/N): ");
                if (newDeck == "Y")
                {
                    SetupGame();
                }
                else break;
            }
            Commens.ClearConsole();

        }

        Commens.WriteLineToUser("Thanks for playing! Goodbye!");
    }

    // Set up the game by getting player details and scoring system
    private void SetupGame()
    {
        // Get player details
        int numberOfPlayers = Commens.GetIntFromUser("Enter number of players (1-4): ", 1, 4);
        playerNames = new string[numberOfPlayers];
        isBot = new bool[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++)
        {
            playerNames[i] = Commens.GetStringFromUser($"Player {i + 1}, please enter name:");
            isBot[i] = Commens.GetYorN($"Is {playerNames[i]} a bot? (Y/N): ") == "Y";
            Commens.WriteLineToUser($"Welcome, {playerNames[i]}!");
        }

        // Get number of decks
        numberOfDecks = Commens.GetIntFromUser("Enter number of decks (1-8): ", 1, 8);

        // Choose scoring system
        string scoringInput = Commens.GetYorN("Do you want to use betting system? (Y/N): ");
        if (scoringInput == "Y")
            scoringChoice = new Betting(playerNames, Commens.GetIntFromUser("Enter starting chips for each player (e.g., 100): ", 1, 10000));
        else
            scoringChoice = new Point_system(playerNames);

        // Initialize game
        game = new(playerNames, isBot, numberOfDecks, scoringChoice);
    }

    static void Main()
    {
        var Program = new Program();
        Program.Run();
    }
        
}



