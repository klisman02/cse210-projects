using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text;

/// <summary>
/// Represents a single entry in the journal.
/// </summary>
class Entry
{
    public DateTime Timestamp { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Mood { get; set; } // 😊 😐 😢
}

/// <summary>
/// Manages a collection of journal entries, providing functionalities like adding, displaying, saving, loading, and searching.
/// </summary>
class Journal
{
    private List<Entry> entries = new List<Entry>();
    private static Random rng = new Random();

    /// <summary>
    /// Adds a new entry to the journal with the current timestamp.
    /// </summary>
    /// <param name="prompt">The prompt or question for the entry.</param>
    /// <param name="response">The user's response to the prompt.</param>
    /// <param name="mood">The user's mood for the entry (e.g., 😊 😐 😢).</param>
    public void AddEntry(string prompt, string response, string mood)
    {
        entries.Add(new Entry
        {
            Timestamp = DateTime.Now,
            Prompt = prompt,
            Response = response,
            Mood = mood
        });
    }

    /// <summary>
    /// Displays all entries currently stored in the journal to the console.
    /// </summary>
    public void DisplayAll()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine($"[{entry.Timestamp}] {entry.Mood} - {entry.Prompt}\n{entry.Response}\n");
        }
    }

    /// <summary>
    /// Saves all current journal entries to a JSON file.
    /// </summary>
    /// <param name="filename">The name of the JSON file to save to.</param>
    public void SaveToJson(string filename)
    {
        string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
    }

    /// <summary>
    /// Loads journal entries from a JSON file, overwriting any existing entries.
    /// </summary>
    /// <param name="filename">The name of the JSON file to load from.</param>
    public void LoadFromJson(string filename)
    {
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            entries = JsonSerializer.Deserialize<List<Entry>>(json) ?? new List<Entry>();
        }
    }

    /// <summary>
    /// Saves all current journal entries to a CSV file.
    /// Handles commas and newlines in prompts and responses for proper CSV formatting.
    /// </summary>
    /// <param name="filename">The name of the CSV file to save to.</param>
    public void SaveToCsv(string filename)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Timestamp,Mood,Prompt,Response"); // CSV header

        foreach (var entry in entries)
        {
            // Escape double quotes and replace newlines for CSV compatibility
            string safePrompt = entry.Prompt.Replace("\"", "\"\"");
            string safeResponse = entry.Response.Replace("\"", "\"\"").Replace("\n", " ");
            sb.AppendLine($"\"{entry.Timestamp}\",\"{entry.Mood}\",\"{safePrompt}\",\"{safeResponse}\"");
        }

        File.WriteAllText(filename, sb.ToString());
    }

    /// <summary>
    /// Searches for entries containing a specific keyword in either the prompt or response.
    /// The search is case-insensitive.
    /// </summary>
    /// <param name="keyword">The keyword to search for.</param>
    public void SearchByKeyword(string keyword)
    {
        foreach (var entry in entries)
        {
            if (entry.Response.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                entry.Prompt.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[{entry.Timestamp}] {entry.Mood} - {entry.Prompt}\n{entry.Response}\n");
            }
        }
    }

    /// <summary>
    /// Searches for entries recorded on a specific date.
    /// </summary>
    /// <param name="date">The date to search for.</param>
    public void SearchByDate(DateTime date)
    {
        foreach (var entry in entries)
        {
            if (entry.Timestamp.Date == date.Date) // Compare only the date part
            {
                Console.WriteLine($"[{entry.Timestamp}] {entry.Mood} - {entry.Prompt}\n{entry.Response}\n");
            }
        }
    }

    /// <summary>
    /// Displays a random entry from the journal.
    /// </summary>
    public void ShowRandomEntry()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("No entries to display.");
            return;
        }

        var entry = entries[rng.Next(entries.Count)]; // Get a random entry
        Console.WriteLine($"📖 Flashback from [{entry.Timestamp}]: {entry.Mood} - {entry.Prompt}\n{entry.Response}\n");
    }

    /// <summary>
    /// Displays statistics about the journal, including total entries, entries by month, and most frequent mood.
    /// </summary>
    public void ShowStats()
    {
        Console.WriteLine($"📊 Total entries: {entries.Count}");

        var entriesByMonth = new Dictionary<string, int>();
        var moodCount = new Dictionary<string, int>();

        foreach (var e in entries)
        {
            string month = e.Timestamp.ToString("yyyy-MM"); // Format date to year-month
            if (!entriesByMonth.ContainsKey(month)) entriesByMonth[month] = 0;
            entriesByMonth[month]++;

            if (!moodCount.ContainsKey(e.Mood)) moodCount[e.Mood] = 0;
            moodCount[e.Mood]++;
        }

        Console.WriteLine("Entries by month:");
        foreach (var kv in entriesByMonth)
            Console.WriteLine($"   {kv.Key}: {kv.Value}");

        Console.WriteLine("Most frequent mood:");
        foreach (var kv in moodCount)
            Console.WriteLine($"   {kv.Key}: {kv.Value}");
    }
}

/// <summary>
/// The main program class that runs the digital journal application.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        string filenameJson = "journal.json"; // Default JSON filename
        bool exit = false;

        Console.WriteLine("Welcome to your Digital Journal! 📝");

        // Main application loop
        while (!exit)
        {
            // Display menu options
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Show all entries");
            Console.WriteLine("3. Save to JSON");
            Console.WriteLine("4. Load from JSON");
            Console.WriteLine("5. Save to CSV");
            Console.WriteLine("6. Search by keyword");
            Console.WriteLine("7. Search by date");
            Console.WriteLine("8. Recall a random entry");
            Console.WriteLine("9. View statistics");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine(); // Read user input

            // Process user's choice
            switch (option)
            {
                case "1":
                    Console.Write("Write your prompt: ");
                    string prompt = Console.ReadLine();

                    Console.Write("Your response: ");
                    string response = Console.ReadLine();

                    Console.Write("How do you feel today? 😊 😐 😢: ");
                    string mood = Console.ReadLine();

                    journal.AddEntry(prompt, response, mood);
                    Console.WriteLine("✅ Entry added!");
                    break;

                case "2":
                    journal.DisplayAll();
                    break;

                case "3":
                    journal.SaveToJson(filenameJson);
                    Console.WriteLine("💾 Journal saved to JSON.");
                    break;

                case "4":
                    journal.LoadFromJson(filenameJson);
                    Console.WriteLine("📂 Journal loaded.");
                    break;

                case "5":
                    Console.Write("CSV filename (e.g., journal.csv): ");
                    string csvFile = Console.ReadLine();
                    journal.SaveToCsv(csvFile);
                    Console.WriteLine("✅ Exported to CSV.");
                    break;

                case "6":
                    Console.Write("Enter keyword: ");
                    string keyword = Console.ReadLine();
                    journal.SearchByKeyword(keyword);
                    break;

                case "7":
                    Console.Write("Enter date (yyyy-mm-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    {
                        journal.SearchByDate(date);
                    }
                    else
                    {
                        Console.WriteLine("❌ Invalid date.");
                    }
                    break;

                case "8":
                    journal.ShowRandomEntry();
                    break;

                case "9":
                    journal.ShowStats();
                    break;

                case "0":
                    exit = true; // Set exit flag to true to end the loop
                    break;

                default:
                    Console.WriteLine("❌ Invalid option.");
                    break;
            }
        }

        Console.WriteLine("👋 Goodbye!");
    }
}