using FocusTime.Models;
using FocusTime.Models.JSON;
using FocusTime.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FocusTime;

public class MainController
{
    public event Action<double> OnExperienceUpdated;
    public event Action<int> OnLevelUpdated;
    public event Action<TimeSpan> OnTimeUpdated;

    private readonly TimerService _timerService;
    private readonly PlayerModel _playerModel;

    public bool IsRunning => _timerService.TimerModel.IsRunning;

    public MainController(IServiceProvider serviceProvider)
    {
        var setings = serviceProvider.GetRequiredService<JsonGameplaySettings>();
        
        _timerService = new TimerService().CallOnTick(TimerOnTick).SetInterval(setings.TimerIntervalSeconds);
        _playerModel = new PlayerModel(setings.DefaultPlayerLevel, TimeSpan.Zero, setings.PlayerTargetExperience, setings.PlayerTargetExperienceProgression);
    }

    public void Start()
    {
        _timerService.Start();
    }

    public void Pause()
    {
        _timerService.Stop();
    }

    public void Reset()
    {
        _timerService.Reset();
    }

    public void HardReset()
    {
        _timerService.Stop();
        _playerModel.Reset();
        OnLevelUpdated?.Invoke(_playerModel.Level);
        OnExperienceUpdated?.Invoke(0);
        Reset();
    }

    private void TimerOnTick(object? sender, EventArgs e)
    {
        var timerModel = _timerService.TimerModel;
        _playerModel.AddExperience(timerModel.TickDelta);
        OnTimeUpdated?.Invoke(timerModel.Elapsed);
        OnExperienceUpdated?.Invoke(_playerModel.Experience.TotalSeconds / _playerModel.TargetExperience.TotalSeconds);
        OnLevelUpdated?.Invoke(_playerModel.Level);
    }
}