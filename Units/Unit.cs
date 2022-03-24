using UnityEngine;

[RequireComponent(typeof(Transform))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected UnitConfig _config;

    protected Transform _transform;

    protected UnitMovement _movement;
    protected Stats _stats = new Stats();

    public Vector2 GetPosition => _transform.position;
    public UnitMovement Movement => _movement;

    private void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _movement = new UnitMovement(transform);

        _transform = GetComponent<Transform>();

        _stats.Initialize(_config);
    }
}
