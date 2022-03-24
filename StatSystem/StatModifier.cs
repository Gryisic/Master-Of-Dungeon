public class StatModifier 
{
    public enum ModifierType
    {
        Flat,
        PercentAdd,
        PercentMultiplier
    }

    public ModifierType GetModifierType => _type;
    public float GetValue => _value;
    public IStatsModifier GetSource => _source;

    private ModifierType _type;
    private float _value;
    private IStatsModifier _source;

    public StatModifier(ModifierType type, float value, IStatsModifier source)
    {
        _type = type;
        _value = value;
        _source = source;
    }

    public StatModifier(ModifierType type, float value) : this(type, value, null) { } 
}
