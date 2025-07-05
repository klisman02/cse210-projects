using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);

        int guess = -1;
        Console.WriteLine("Welcome to the Magic Number Game!");
        Console.WriteLine("I have selected a magic number between 1 and 100.");
        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            if (magicNumber > guess)
            {
                Console.WriteLine("Higher");
            }
            else if (magicNumber < guess)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }

        }
    }
}       
// This code implements a simple console-based game where the user has to guess a randomly generated number between 1 and 100.
// The program provides feedback on whether the guess is too high or too low until the user guesses the correct number.
// It uses a `while` loop to continue prompting the user for guesses until the correct number           