// Program.cs
using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    private static List<Goal> _goals = new List<Goal>();
    private static int _score = 0;
    private static int _level = 1; // Exceeding core requirements

    // Exceeding core requirements: Simple leveling system
    private static void CheckLevelUp()
    {
        int newLevel = (_score / 10000) + 1;
        if (newLevel > _level)
        {
            _level = newLevel;
            Console.WriteLine($"\n*** Congratulations! You leveled up to Level {_level}! ***");
        }
    }

    public static void Main(string[] args)
    {
        string choice = "";
        while (choice != "6")
        {
            Console.Clear();
            Console.WriteLine($"You have {_score} points and are at Level {_level}.");
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select an option from the menu: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    Console.WriteLine("\nThanks for using Eternal Quest. Goodbye!");
                    return;
                default:
                    Console.WriteLine("\nInvalid option. Press Enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private static void CreateGoal()
    {
        Console.Clear();
        Console.WriteLine("The types of goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        string goalType = Console.ReadLine();

        Console.Write("What is the short name of your goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a brief description of your goal? ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());

        Goal newGoal = null;
        switch (goalType)
        {
            case "1":
                newGoal = new SimpleGoal(name, description, points);
                break;
            case "2":
                newGoal = new EternalGoal(name, description, points);
                break;
            case "3":
                Console.Write("How many times does this goal need to be completed for a bonus? ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("What is the bonus value for completing it? ");
                int bonus = int.Parse(Console.ReadLine());
                newGoal = new ChecklistGoal(name, description, points, target, bonus);
                break;
            default:
                Console.WriteLine("\nInvalid goal type. Goal creation cancelled.");
                Console.ReadLine();
                return;
        }

        _goals.Add(newGoal);
        Console.WriteLine("\nGoal created successfully! Press Enter to continue...");
        Console.ReadLine();
    }

    private static void ListGoals()
    {
        Console.Clear();
        Console.WriteLine("Your Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }

    private static void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
        Console.WriteLine("\nGoals saved successfully! Press Enter to continue...");
        Console.ReadLine();
    }

    private static void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();
        
        if (!File.Exists(filename))
        {
            Console.WriteLine("\nFile not found. Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        _score = int.Parse(lines[0]);
        _goals.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(':');
            string goalType = parts[0];
            string[] goalData = parts[1].Split('|');

            switch (goalType)
            {
                case "SimpleGoal":
                    _goals.Add(new SimpleGoal(goalData[0], goalData[1], int.Parse(goalData[2]), bool.Parse(goalData[3])));
                    break;
                case "EternalGoal":
                    _goals.Add(new EternalGoal(goalData[0], goalData[1], int.Parse(goalData[2])));
                    break;
                case "ChecklistGoal":
                    _goals.Add(new ChecklistGoal(goalData[0], goalData[1], int.Parse(goalData[2]), int.Parse(goalData[3]), int.Parse(goalData[4]), int.Parse(goalData[5])));
                    break;
            }
        }
        Console.WriteLine("\nGoals loaded successfully! Press Enter to continue...");
        CheckLevelUp(); // Check for level up on load
        Console.ReadLine();
    }

    private static void RecordEvent()
    {
        Console.Clear();
        Console.WriteLine("Your Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
        Console.Write("Which goal did you accomplish? ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= _goals.Count)
        {
            int pointsEarned = _goals[index - 1].RecordEvent();
            _score += pointsEarned;
            CheckLevelUp(); // Check for level up after earning points
            Console.WriteLine($"\nYou earned {pointsEarned} points!");
        }
        else
        {
            Console.WriteLine("\nInvalid input.");
        }
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}