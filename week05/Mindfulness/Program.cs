// Content for a SINGLE C# file
using System;
using System.Collections.Generic;
using System.Threading;

// Comment: Code to exceed requirements, keeping a log of how many activities have been completed.
class Program
{
    static int _activitiesCompleted = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Mindfulness Program!");
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            Activity activity = null;
            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectingActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    Console.WriteLine($"You completed {_activitiesCompleted} activities this session. Great job!");
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Thread.Sleep(2000);
                    break;
            }

            if (activity != null)
            {
                activity.Run();
                _activitiesCompleted++;
            }
        }
    }
}

public class Activity
{
    // Private member variables for encapsulation
    private string _name;
    private string _description;
    protected int _duration;

    // Constructor to set the name and description
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    // Displays the starting message, description, and prompts for duration
    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");
        _duration = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
    }

    // Displays the ending message
    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        ShowSpinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed {_duration} seconds of the {_name} activity.");
        ShowSpinner(4);
    }

    // Shows a simple spinner animation
    public void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(500);
        }
        Console.Write("\b \b");
    }

    // Shows a countdown timer
    public void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
            Console.Write("\b\b\b");
        }
        Console.Write("   \b\b\b");
    }

    // Virtual method for the main activity logic, to be overridden by derived classes
    public virtual void Run()
    {
        // This method will be overridden
    }
}

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void Run()
    {
        DisplayStartingMessage();

        DateTime startTime = DateTime.Now;
        DateTime futureTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < futureTime)
        {
            Console.Write("Breathe in... ");
            ShowCountdown(4);
            Console.WriteLine();

            Console.Write("Breathe out... ");
            ShowCountdown(6);
            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}

public class ReflectingActivity : Activity
{
    private List<string> _prompts;
    private List<string> _questions;
    private Random _random;

    public ReflectingActivity()
        : base("Reflecting Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
        _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };
        _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };
        _random = new Random();
    }

    public override void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine($"--- {_prompts[_random.Next(_prompts.Count)]} ---");
        Console.WriteLine();
        Console.WriteLine("When you have something in mind, press Enter to continue.");
        Console.ReadLine();

        Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
        Console.Write("You may begin in: ");
        ShowCountdown(5);

        Console.Clear();
        DateTime startTime = DateTime.Now;
        DateTime futureTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < futureTime)
        {
            Console.WriteLine($"> {_questions[_random.Next(_questions.Count)]}");
            ShowSpinner(5);
        }

        DisplayEndingMessage();
    }
}

public class ListingActivity : Activity
{
    private List<string> _prompts;
    private Random _random;

    public ListingActivity()
        : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt peace or inspiration this month?",
            "Who are some of your personal heroes?"
        };
        _random = new Random();
    }

    public override void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine($"--- {_prompts[_random.Next(_prompts.Count)]} ---");
        Console.WriteLine();
        Console.Write("You may begin in: ");
        ShowCountdown(5);

        Console.WriteLine();
        Console.WriteLine("Start listing items:");

        DateTime startTime = DateTime.Now;
        DateTime futureTime = startTime.AddSeconds(_duration);
        int itemCount = 0;

        // The loop now uses Console.KeyAvailable to read user input
        // while the time does not run out, without blocking the program.
        while (DateTime.Now < futureTime)
        {
            // Checks if a key has been pressed
            if (Console.KeyAvailable)
            {
                // Reads the input line and increments the count if not empty
                string item = Console.ReadLine();
                if (!string.IsNullOrEmpty(item))
                {
                    itemCount++;
                }
            }
        }

        Console.WriteLine($"You listed {itemCount} items!");
        DisplayEndingMessage();
    }
}
