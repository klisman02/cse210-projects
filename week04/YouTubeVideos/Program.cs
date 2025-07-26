using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // --- Video 1 ---
        Video video1 = new Video("C# Programming Basics", "Code Academy", 1200, 15000, 800, 10);
        video1.AddComment(new Comment("Alice", "Great introduction to C#!"));
        video1.AddComment(new Comment("Bob", "Very clear explanations."));
        video1.AddComment(new Comment("Charlie", "Helped me understand variables better."));
        videos.Add(video1);

        // --- Video 2 ---
        Video video2 = new Video("Introduction to OOP", "Tech Guru", 1800, 18000, 1200, 20);
        video2.AddComment(new Comment("David", "The abstraction part was confusing at first, but makes sense now."));
        video2.AddComment(new Comment("Eve", "Excellent examples for encapsulation."));
        video2.AddComment(new Comment("Frank", "Could you do a video on design patterns next?"));
        video2.AddComment(new Comment("Grace", "This really helped me with my assignment."));
        videos.Add(video2);

        // --- Video 3 ---
        Video video3 = new Video("Building a Simple Web App", "Dev Journey", 2700, 25000, 1700, 30);
        video3.AddComment(new Comment("Heidi", "I followed along and it worked! Thanks!"));
        video3.AddComment(new Comment("Ivan", "What framework did you use for the frontend?"));
        video3.AddComment(new Comment("Judy", "Always appreciate your clear tutorials."));
        videos.Add(video3);

        // --- Video 4 ---
        Video video4 = new Video("Data Structures in Python", "Python Hub", 2100, 22000, 1600, 25);
        video4.AddComment(new Comment("Kevin", "Even though it's Python, the concepts are universal."));
        video4.AddComment(new Comment("Liam", "Well explained trees and graphs!"));
        video4.AddComment(new Comment("Mia", "Wish there was a C# version, but still useful."));
        videos.Add(video4);

        // Sort by number of comments (descending)
        List<Video> sortedByComments = videos.OrderByDescending(v => v.GetNumberOfComments()).ToList();

        // Display on console
        Console.WriteLine("--- YouTube Video Information ---\n");
        foreach (Video video in sortedByComments)
        {
            DisplayVideoInfo(video);
            Console.WriteLine(); // Blank line for readability
        }

        Console.WriteLine("--- End of Program ---");

        // Export to file
        ExportToTextFile(sortedByComments, "YouTubeVideos/videos.txt");
        //ExportToTextFile(sortedByComments, Path.Combine(Directory.GetCurrentDirectory(), "videos.txt"));
    }

    // Helper method to display video information to the console
    static void DisplayVideoInfo(Video video)
    {
        Console.WriteLine($"Title: {video.Title}");
        Console.WriteLine($"Author: {video.Author}");
        Console.WriteLine($"Length: {video.Length / 60}m {video.Length % 60}s");
        Console.WriteLine($"Views: {video.Views}");
        Console.WriteLine($"Likes: {video.Likes}");
        Console.WriteLine($"Dislikes: {video.Dislikes}");
        Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        foreach (Comment comment in video.GetComments())
        {
            Console.WriteLine($"   - {comment.CommenterName}: \"{comment.CommentText}\"");
        }
    }

    // Helper method to export video information to a text file
static void ExportToTextFile(List<Video> videos, string filename)
{
    try
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("--- YouTube Video Information ---\n");

            foreach (Video video in videos)
            {
                writer.WriteLine($"Title: {video.Title}");
                writer.WriteLine($"Author: {video.Author}");
                writer.WriteLine($"Length: {video.Length / 60}m {video.Length % 60}s");
                writer.WriteLine($"Views: {video.Views}");
                writer.WriteLine($"Likes: {video.Likes}");
                writer.WriteLine($"Dislikes: {video.Dislikes}");
                writer.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
                writer.WriteLine("Comments:");
                foreach (Comment comment in video.GetComments())
                {
                    writer.WriteLine($"  - {comment.CommenterName}: \"{comment.CommentText}\"");
                }
                writer.WriteLine();
            }

            writer.WriteLine("--- End of Export ---");
        }

        Console.WriteLine($"\n✅ File exported successfully to: {filename}");
    }
    catch (UnauthorizedAccessException)
    {
        Console.WriteLine($"❌ Error: No permission to record. Path: '{filename}'.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error to export: {ex.Message}");
    }
}

}

// Comment Class
public class Comment
{
    // Using auto-implemented properties with private setters for immutability after construction
    public string CommenterName { get; }
    public string CommentText { get; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }
}

// Video Class
public class Video
{
    // Using auto-implemented properties with private setters for immutability after construction
    public string Title { get; }
    public string Author { get; }
    public int Length { get; } // in seconds
    public int Views { get; }
    public int Likes { get; }
    public int Dislikes { get; }

    // Private list to encapsulate comment storage
    private List<Comment> comments;

    public Video(string title, string author, int length, int views, int likes, int dislikes)
    {
        Title = title;
        Author = author;
        Length = length;
        Views = views;
        Likes = likes;
        Dislikes = dislikes;
        comments = new List<Comment>(); // Initialize the list
    }

    // Method to add a new comment to this video
    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    // Method to return the number of comments for this video
    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    // Method to get all comments for display purposes
    // Returns the internal list directly for simplicity as per common assignment patterns
    public List<Comment> GetComments()
    {
        return comments;
    }
}