using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [SerializeField] private Tilemap _floor;
    [SerializeField] private Tilemap _walls;

    private List<Unit> _units = new List<Unit>();

    public Tilemap GetFloor => _floor;
    public Tilemap GetWalls => _walls;
    public List<Unit> GetUnits => _units;

    public void Enter() 
    {
        
    }

    public void Exit() 
    {
        
    }
}
