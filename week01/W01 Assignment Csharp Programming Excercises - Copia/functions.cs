using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcomeMessage();
        // Prompt the user for their name and favorite number
        // Square the number and display the result     
        string userName = PromptUserName();
        int userNumber = PromptUserNumber();
        // Square the number
        // Display the result   
        int squaredNumber = SquareNumber(userNumber);
        // Display the result           
        DisplayResult(userName, squaredNumber);
    }
    // Methods  
    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty. Please try again.");
            return PromptUserName(); // Recursively prompt until a valid name is entered
        }           
        return name;
    }

    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int number = int.Parse(Console.ReadLine());

        return number;
    }
// Square the number and return the result      
    static int SquareNumber(int number)
    {
        int square = number * number;
        return square;
    }
    // Display the result
    static void DisplayResult(string name, int square)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
    }
}