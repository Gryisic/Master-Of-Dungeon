using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay / Config / Unit", fileName = "Unit Config")]
public class UnitConfig : ScriptableObject
{
    public string Name => _name;
    public int MaxHealth => _maxHealth;
    public int Health => _health;
    public int MovementRange => _movementRange;
    public int Strength => _strength;
    public int Defence => _defence;
    public int Agility => _agility;
    public int Accuracy => _accuracy;

    [SerializeField] private string _name;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;
    [SerializeField] private int _movementRange;
    [SerializeField] private int _strength;
    [SerializeField] private int _defence;
    [SerializeField] private int _agility;
    [SerializeField] private int _accuracy;
}
