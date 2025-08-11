using System;

public class Cycling : Activity
{
    private double _speed;

    public Cycling(DateTime date, int minutes, double speed)
        : base("Cycling", date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return (GetSpeed() * GetMinutes()) / 60;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        return 60 / GetSpeed();
    }
}