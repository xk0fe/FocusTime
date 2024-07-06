using FocusTime.Constants;
using FocusTime.Upgrades;

namespace FocusTime.Models;

public class PlayerModel
{
    public int Level { get; private set; }

    public TimeSpan Experience { get; private set; }
    public TimeSpan TargetExperience { get; private set; }

    private readonly List<Upgrade> _upgrades = new();
    private readonly Dictionary<int, TimeSpan> _progression;

    public PlayerModel(int level, TimeSpan experience, TimeSpan targetExperience, Dictionary<int, TimeSpan> progression)
    {
        Level = level;
        Experience = experience;
        TargetExperience = targetExperience;
        _progression = progression;
    }

    private void LevelUp()
    {
        Level++;
        Experience -= TargetExperience;

        var progressionCount = _progression.Count;
        if (progressionCount > 0)
        {
            TargetExperience += _progression[Level % progressionCount];
        }
    }

    public void AddExperience(TimeSpan experience)
    {
        Experience += experience;
        
        var levelProgress = Experience.TotalSeconds / TargetExperience.TotalSeconds;
        if (levelProgress >= PlayerConstants.PROGRESS_BAR_MAXIMUM)
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