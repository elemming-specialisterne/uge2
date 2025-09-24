class Program
{
    private int numberOfDecks = 1;
    private string[] playerNames = ["Player1"];
    private string[] botNames = [];

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

        Game game = new(playerNames, botNames, numberOfDecks);
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
                    game = new(playerNames, botNames, numberOfDecks);
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
        for (int i = 0; i < numberOfPlayers; i++)
        {
            playerNames[i] = Commens.GetStringFromUser($"Player {i + 1}, please enter your name:");
            Commens.WriteLineToUser($"Welcome, {playerNames[i]}!");
        }

        int numberOfBots = Commens.GetIntFromUser("Enter number of bots excluding Dealer (0-3): ", 0, 3);
        botNames = new string[numberOfBots];
        for (int i = 0; i < numberOfBots; i++)
        {
            botNames[i] = Commens.GetStringFromUser($"Enter name of Bot {i + 1}:");
            Commens.WriteLineToUser($"Give {botNames[i]} a warm welcome!");
        }
        numberOfDecks = Commens.GetIntFromUser("Enter number of decks (1-8): ", 1, 8);
    }

    static void Main()
    {
        var Program = new Program();
        Program.Run();
    }
        
}



