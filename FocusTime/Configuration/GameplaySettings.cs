namespace FocusTime.Configuration;

public static class GameplaySettings
{
    public const float TIMER_INTERVAL_SECONDS = 0.035f;
    public const float PROGRESS_BAR_MAXIMUM = 1.0f;
    public const int DEFAULT_PLAYER_LEVEL = 0;

    public static readonly TimeSpan PLAYER_TARGET_EXPERIENCE = new(00, 00, 05);

    public static readonly Dictionary<int, TimeSpan> PLAYER_TARGET_EXPERIENCE_PROGRESSION =
        new()
        {
            { 0, new TimeSpan(00, 05, 00) },
            { 1, new TimeSpan(00, 10, 00) },
            { 2, new TimeSpan(00, 15, 00) }
        };
}