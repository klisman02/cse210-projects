using System;

public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int minutes, int laps)
        : base("Swimming", date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        // Distance (km) = swimming laps * 50 / 1000
        return (_laps * 50.0) / 1000;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / GetMinutes()) * 60;
    }

    public override double GetPace()
    {
        return GetMinutes() / GetDistance();
    }
}