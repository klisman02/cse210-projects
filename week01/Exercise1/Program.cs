class Program
{
    static void Main(string[] args)
    {
        // This program prompts the user for their first and last name, then outputs their name in a specific format.
        Console.WriteLine("Hello, World!");
        Console.WriteLine("What is your name? ");
        string first = Console.ReadLine();

        Console.WriteLine("What is your last name? ");
        string last = Console.ReadLine();

        Console.WriteLine($"Your name is {last}, {first} {last}.");
    }
}
// This program demonstrates basic input and output in C#.
// It uses Console.WriteLine to display messages and Console.ReadLine to read user input.