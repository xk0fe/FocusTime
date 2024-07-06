namespace FocusTime.Models;

public class TimerModel
{
    public TimeSpan Elapsed { get; private set; }
    public TimeSpan TickDelta { get; private set; }
    public bool IsRunning { get; private set; }

    public TimerModel()
    {
        Elapsed = TimeSpan.Zero;
        TickDelta = TimeSpan.Zero;
        IsRunning = false;
    }

    public void Start()
    {
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Reset()
    {
        Elapsed = TimeSpan.Zero;
    }

    public void UpdateElapsed(TimeSpan delta)
    {
        TickDelta = delta;
        Elapsed += TickDelta;
    }
}