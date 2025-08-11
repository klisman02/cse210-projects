using System;

public class Activity
{
    private DateTime _date;
    private int _minutes;
    private string _activityType;

    public Activity(string activityType, DateTime date, int minutes)
    {
        _activityType = activityType;
        _date = date;
        _minutes = minutes;
    }

    public int GetMinutes()
    {
        return _minutes;
    }

    // These methods are virtual so they can be overridden
    public virtual double GetDistance()
    {
        return 0; // Default implementation
    }

    public virtual double GetSpeed()
    {
        return 0; // Default implementation
    }

    public virtual double GetPace()
    {
        return 0; // Default implementation
    }

    public virtual string GetSummary()
    {
        return $"{_date.ToString("dd MMM yyyy")} {_activityType} ({_minutes} min): " +
            $"Distance {GetDistance():F1} km, Speed: {GetSpeed():F1} kph, Pace: {GetPace():F1} min per km";
    }
}