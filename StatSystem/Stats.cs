using System.Collections.Generic;

public class Stats 
{
    public IReadOnlyList<Stat> GetStats => _stats;

    private List<Stat> _stats = new List<Stat>();

    public void Initialize(UnitConfig _config)
    {
        _stats.Add(new Stat(StatType.MaxHealth, _config.MaxHealth));
        _stats.Add(new Stat(StatType.Health, _config.Health));
        _stats.Add(new Stat(StatType.MovementRange, _config.MovementRange));
        _stats.Add(new Stat(StatType.Strength, _config.Strength));
        _stats.Add(new Stat(StatType.Defence, _config.Defence));
        _stats.Add(new Stat(StatType.Agility, _config.Agility));
        _stats.Add(new Stat(StatType.Accuracy, _config.Accuracy));
    }
}
