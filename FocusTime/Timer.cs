using System.Windows.Threading;

namespace FocusTime;

public class Timer
{
    public TimeSpan Elapsed { get; private set; }
    public TimeSpan TickDelta { get; private set; }
    public bool IsRunning { get; private set; }
    private DispatcherTimer _timer;
    private DateTime _lastTickTime;

    public Timer()
    {
        IsRunning = false;
        _timer = new DispatcherTimer();
        _timer.Tick += OnTickInternal;
    }
    
    public Timer CallOnTick(EventHandler tickFunction)
    {
        _timer.Tick += tickFunction;

        return this;
    }

    public Timer SetInterval(float seconds)
    {
        _timer.Interval = TimeSpan.FromSeconds(seconds);

        return this;
    }

    public void Start()
    {
        IsRunning = true;
        
        var timeNow = GetCurrentTime();
        _lastTickTime = timeNow;
        _timer.Start();
    }

    public void Stop()
    {
        IsRunning = false;
        _timer.Stop();
    }

    public void Pause()
    {
        
    }

    public void Reset()
    {
        Elapsed = TimeSpan.Zero;
    }

    private void OnTickInternal(object sender, EventArgs e)
    {
        var timeNow = GetCurrentTime();
        TickDelta = timeNow - _lastTickTime;
        _lastTickTime = timeNow;
        Elapsed += TickDelta;
    }
    
    // todo remove from Timer class
    private DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}