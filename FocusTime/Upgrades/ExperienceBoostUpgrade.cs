namespace FocusTime.Upgrades;

/// <summary>
/// Experience boost is calculated based on PERCENTAGE from player's TARGET EXPERIENCE
/// </summary>
public class ExperienceBoostUpgrade : Upgrade
{
    private readonly float _percentage;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <param name="percentage">Must be presented as 00.X where is X is the target percentage</param>
    public ExperienceBoostUpgrade(string name, string id, float percentage) : base(name, id)
    {
        _percentage = _percentage;
    }

    public override void OnActivate(Player player)
    {
    }

    public override void Update(Player player)
    {
        player.AddExperience(player.TargetExperience * _percentage);
    }
}