public static class Commens
{
    public static void ClearConsole()
    {
        Console.Clear();
    }

    public static void WaitForUser()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    public static string GetYorN(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim().ToUpperInvariant();
            if (input == "Y" || input == "N")
                return input;
            Console.WriteLine("Invalid input. Please enter Y or N.");
        }
    }

    public static string GetHorS(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim().ToUpperInvariant();
            if (input == "H" || input == "S")
                return input;
            Console.WriteLine("Invalid input. Please enter H or S.");
        }
    }

    public static string GetStringFromUser(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(input))
                return input;
            Console.WriteLine("Input cannot be empty. Please try again.");
        }
    }

    public static int GetIntFromUser(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();
            if (int.TryParse(input, out int value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"Invalid input. Please enter an integer between {min} and {max}.");
        }
    }

    public static void WriteLineToUser(string message)
    {
        Console.WriteLine(message);
    }
}