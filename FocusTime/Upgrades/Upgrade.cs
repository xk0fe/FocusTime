using FocusTime.Models;

namespace FocusTime.Upgrades;

public abstract class Upgrade
{
    public string Name { get; private set; }
    public string Id { get; private set; }
    
    protected Upgrade(string name, string id)
    {
        Name = name;
        Id = id;
    }

    public abstract void OnActivate(PlayerModel playerModel);
    
    public abstract void Update(PlayerModel playerModel);
}