// Utility class for common console operations to display information to the user and get input
// This class handles all interactions with the user via the console
// It could be replaced with a GUI or web interface in the future
public static class Commens
{
    // Clear the console
    public static void ClearConsole()
    {
        Console.Clear();
    }

    // Wait for user to press any key
    public static void WaitForUser()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    // Get a Yes or No response from the user
    public static string GetYorN(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim().ToUpperInvariant();
            if (input == "Y" || input == "N")
                return input;
            Console.WriteLine("Invalid input. Please enter Y or N.");
        }
    }

    // Get a Hit or Stand response from the user
    public static string GetHorS(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim().ToUpperInvariant();
            if (input == "H" || input == "S")
                return input;
            Console.WriteLine("Invalid input. Please enter H or S.");
        }
    }

    // Get a non-empty string from the user
    public static string GetStringFromUser(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(input))
                return input;
            Console.WriteLine("Input cannot be empty. Please try again.");
        }
    }

    // Get an integer within a specified range from the user
    public static int GetIntFromUser(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();
            if (int.TryParse(input, out int value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"Invalid input. Please enter an integer between {min} and {max}.");
        }
    }

    // Write a message to the console
    public static void WriteLineToUser(string message)
    {
        Console.WriteLine(message);
    }
}