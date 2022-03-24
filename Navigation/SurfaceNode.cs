using UnityEngine;

public class SurfaceNode 
{
    private Vector2 _position;
    private SurfaceNode _parentNode;

    private int _generalCost = int.MaxValue;
    private int _heuristicCost;

    public Vector2 GetPosition => _position;
    public SurfaceNode ParentNode => _parentNode;

    public int GetFinalCost => _generalCost + _heuristicCost;
    public int GetGeneralCost => _generalCost;

    public SurfaceNode(Vector2 position)
    {
        _position = position;
    }

    public void SetCost(int generalCost, int heuristicCost) 
    {
        _generalCost = generalCost;
        _heuristicCost = heuristicCost;
    }

    public void Clear() 
    {
        _generalCost = int.MaxValue;
        _heuristicCost = int.MaxValue;
        _parentNode = null;
    }

    public void SetParentNode(SurfaceNode parentNode) => _parentNode = parentNode;
}
