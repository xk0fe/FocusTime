using System.Windows.Threading;
using FocusTime.Models;

namespace FocusTime.Services;

public class TimerService
{
    public readonly TimerModel TimerModel;
    private readonly DispatcherTimer _dispatcherTimer;
    private DateTime _lastTickTime;

    public TimerService()
    {
        TimerModel = new TimerModel();
        _dispatcherTimer = new DispatcherTimer();
        _dispatcherTimer.Tick += OnTickInternal;
    }
    
    public TimerService(TimerModel timerModel)
    {
        TimerModel = timerModel;
        _dispatcherTimer = new DispatcherTimer();
        _dispatcherTimer.Tick += OnTickInternal;
    }

    public TimerService CallOnTick(EventHandler tickFunction)
    {
        _dispatcherTimer.Tick += tickFunction;
        return this;
    }

    public TimerService SetInterval(float seconds)
    {
        _dispatcherTimer.Interval = TimeSpan.FromSeconds(seconds);
        return this;
    }

    public void Start()
    {
        TimerModel.Start();
        _lastTickTime = GetCurrentTime();
        _dispatcherTimer.Start();
    }

    public void Stop()
    {
        TimerModel.Stop();
        _dispatcherTimer.Stop();
    }

    public void Reset()
    {
        TimerModel.Reset();
    }

    private void OnTickInternal(object? sender, EventArgs e)
    {
        var timeNow = GetCurrentTime();
        var tickDelta = timeNow - _lastTickTime;
        _lastTickTime = timeNow;
        TimerModel.UpdateElapsed(tickDelta);
    }

    private DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}