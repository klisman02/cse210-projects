using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        // Create instances of each activity type
        Running runningActivity = new Running(new DateTime(2023, 11, 03), 30, 4.8);
        Cycling cyclingActivity = new Cycling(new DateTime(2023, 11, 03), 30, 9.7);
        Swimming swimmingActivity = new Swimming(new DateTime(2023, 11, 03), 30, 10);

        // Add them to the list of base type
        activities.Add(runningActivity);
        activities.Add(cyclingActivity);
        activities.Add(swimmingActivity);

        // Iterate through the list and call the common GetSummary method
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}