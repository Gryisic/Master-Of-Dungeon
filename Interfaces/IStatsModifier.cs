using System.Collections.Generic;

public interface IStatsModifier 
{
    IEnumerable<StatType> StatsToModify();
    Dictionary<StatModifier.ModifierType, float> ModifierValue();
}
