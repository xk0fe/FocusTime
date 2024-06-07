using FocusTime.Configuration;

namespace FocusTime;

public class MainController
{
    public event Action<double> OnExperienceUpdated;
    public event Action<int> OnLevelUpdated;
    public event Action<TimeSpan> OnTimeUpdated;

    private readonly Timer _timer;
    private readonly Player _player;

    public bool IsRunning => _timer.IsRunning;

    public MainController()
    {
        _timer = new Timer().CallOnTick(TimerOnTick).SetInterval(GameplaySettings.TIMER_INTERVAL_SECONDS);
        _player = new Player(GameplaySettings.DEFAULT_PLAYER_LEVEL, TimeSpan.Zero, GameplaySettings.PLAYER_TARGET_EXPERIENCE);
    }

    public void Start()
    {
        _timer.Start();
    }

    public void Pause()
    {
        _timer.Stop();
    }

    public void Reset()
    {
        _timer.Reset();
    }

    public void HardReset()
    {
        _timer.Stop();
        _player.Reset();
        OnLevelUpdated?.Invoke(_player.Level);
        OnExperienceUpdated?.Invoke(0);
        Reset();
    }

    private void TimerOnTick(object sender, EventArgs e)
    {
        _player.AddExperience(_timer.TickDelta);
        OnTimeUpdated?.Invoke(_timer.Elapsed);
        OnExperienceUpdated?.Invoke(_player.Experience.TotalSeconds / _player.TargetExperience.TotalSeconds);
        OnLevelUpdated?.Invoke(_player.Level);
    }
}