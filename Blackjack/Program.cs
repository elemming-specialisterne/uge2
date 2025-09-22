Commens.WriteLineToUser("Welcome to Blackjack!");
Commens.WriteLineToUser("This is a console-based Blackjack game.");

Commens.WriteLineToUser("Press Enter to start the game...");
Commens.WaitForUser();
Commens.ClearConsole();

int numberOfPLayers = Commens.GetIntFromUser("Enter number of players (1-4): ", 1, 4);
int numberOfBots = Commens.GetIntFromUser("Enter number of bots excluding Dealer (0-3): ", 0, 3);
int numberOfDecks = Commens.GetIntFromUser("Enter number of decks (1-8): ", 1, 8);


Game game = new(numberOfPLayers, numberOfBots, numberOfDecks);
while (true)
{
    Commens.ClearConsole();
    Commens.WriteLineToUser("Starting a new round...");
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
                numberOfPLayers = Commens.GetIntFromUser("Enter number of players (1-4): ", 1, 4);
                numberOfBots = Commens.GetIntFromUser("Enter number of bots excluding Dealer (0-3): ", 0, 3);
                numberOfDecks = Commens.GetIntFromUser("Enter number of decks (1-8): ", 1, 8);
            }
            game = new(numberOfPLayers, numberOfBots, numberOfDecks);
        }
    }
    Commens.ClearConsole();

}

Commens.WriteLineToUser("Thanks for playing! Goodbye!");



