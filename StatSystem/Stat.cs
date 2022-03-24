using System.Collections.Generic;
using UnityEngine;

public class Stat 
{
    public int Value => FinalValue();
    public StatType GetStatType => _type;

    private StatType _type;
    private List<StatModifier> _modifiers = new List<StatModifier>();

    private int _baseValue;

    public Stat(StatType type, int baseValue)
    {
        _type = type;
        _baseValue = baseValue;
    }

    public void AddModifier(StatModifier modifier) => _modifiers.Add(modifier);

    public void RemoveAllModifiers() => _modifiers.Clear();

    public bool RemoveModifier(StatModifier modifier) => _modifiers.Remove(modifier);

    public void RemoveAllModifiersFromSource(IStatsModifier source)
    {
        foreach (var modifier in _modifiers)
        {
            if (modifier.GetSource == source)
                _modifiers.Remove(modifier);
        }
    }

    private int FinalValue()
    {
        float bufer = _baseValue;

        if (_modifiers.Count > 0)
        {
            foreach (var modifier in _modifiers)
            {
                switch (modifier.GetModifierType)
                {
                    case StatModifier.ModifierType.Flat :
                        bufer += modifier.GetValue;
                        break;
                    case StatModifier.ModifierType.PercentAdd:
                        bufer *= 1 + modifier.GetValue;
                        break;
                    case StatModifier.ModifierType.PercentMultiplier:
                        bufer *= modifier.GetValue;
                        break;
                }
            }
        }

        return Mathf.RoundToInt(bufer);
    }
}
