namespace FocusTime.Models.JSON;

public class JsonGameplaySettings
{
    public float TimerIntervalSeconds { get; set; }
    public float ProgressBarMaximum { get; set; }
    public int DefaultPlayerLevel { get; set; }
    public TimeSpan PlayerTargetExperience { get; set; }
    public Dictionary<int, TimeSpan> PlayerTargetExperienceProgression { get; set; }
}