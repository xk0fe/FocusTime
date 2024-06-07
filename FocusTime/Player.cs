using FocusTime.Configuration;
using FocusTime.Upgrades;

namespace FocusTime;

public class Player
{
    public int Level { get; private set; }

    public TimeSpan Experience { get; private set; }
    public TimeSpan TargetExperience { get; private set; }

    private readonly List<Upgrade> _upgrades = new();

    public Player(int level, TimeSpan experience, TimeSpan targetExperience)
    {
        Level = level;
        Experience = experience;
        TargetExperience = targetExperience;
    }

    private void LevelUp()
    {
        Level++;
        Experience -= TargetExperience;

        var progression = GameplaySettings.PLAYER_TARGET_EXPERIENCE_PROGRESSION;
        var progressionCount = progression.Count;
        if (progressionCount > 0)
        {
            TargetExperience += progression[Level % progressionCount];
        }
    }

    public void AddExperience(TimeSpan experience)
    {
        Experience += experience;
        
        var levelProgress = Experience.TotalSeconds / TargetExperience.TotalSeconds;
        if (levelProgress >= GameplaySettings.PROGRESS_BAR_MAXIMUM)
        {
            LevelUp();
        }
    }

    // todo
    public void AddCoins()
    {
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        upgrade.OnActivate(this);
        
        _upgrades.Add(upgrade);
    }

    public void Update()
    {
        foreach (var upgrade in _upgrades)
        {
            upgrade.Update(this);
        }
    }

    public void Reset()
    {
        Level = 0;
        Experience = TimeSpan.Zero;
    }
}