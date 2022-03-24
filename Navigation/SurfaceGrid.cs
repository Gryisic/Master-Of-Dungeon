using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SurfaceGrid 
{
    private const float DISTANCE_BETWEEN_NODES = 1;

    public IReadOnlyDictionary<Vector2, SurfaceNode> GetNodes => _nodes;

    private Dictionary<Vector2, SurfaceNode> _nodes = new Dictionary<Vector2, SurfaceNode>();

    public void Build(Vector2 from, Vector2 to, Tilemap tilemap)
    {
        _nodes.Clear();

        var directionX = MathExtension.Direction(from.x, to.x);
        var directionY = MathExtension.Direction(from.y, to.y);

        for (float x = from.x; x != to.x + directionX; x += DISTANCE_BETWEEN_NODES * directionX)
        {
            for (float y = from.y; y != to.y + directionY; y += DISTANCE_BETWEEN_NODES * directionY)
            {
                var coordinates = new Vector3Int(MathExtension.RoundedValue(x), 
                    MathExtension.RoundedValue(y), 0);

                if (tilemap.GetTile(coordinates))
                    GenerateNode(tilemap.GetCellCenterWorld(coordinates));
            }
        }
    }

    public void ClearNodes() 
    {
        foreach (var node in _nodes.Values)
        {
            node.Clear();
        }
    }

    public List<SurfaceNode> GetNeighbours(SurfaceNode node) 
    {
        var position = node.GetPosition;
        var neighbours = new List<SurfaceNode>();

        if (_nodes.ContainsKey(position))
        {
            neighbours.Add(VerifiedNode(new Vector2(position.x + 1, position.y - 1)));
            neighbours.Add(VerifiedNode(new Vector2(position.x + 1, position.y)));
            neighbours.Add(VerifiedNode(new Vector2(position.x + 1, position.y + 1)));
            neighbours.Add(VerifiedNode(new Vector2(position.x, position.y + 1)));
            neighbours.Add(VerifiedNode(new Vector2(position.x - 1, position.y + 1)));
            neighbours.Add(VerifiedNode(new Vector2(position.x - 1, position.y)));
            neighbours.Add(VerifiedNode(new Vector2(position.x - 1, position.y - 1)));
            neighbours.Add(VerifiedNode(new Vector2(position.x, position.y - 1)));

            return neighbours;
        }

        return null;
    }

    private void GenerateNode(Vector2 position) => _nodes.Add(position, new SurfaceNode(position)); 

    private SurfaceNode VerifiedNode(Vector2 position) 
    {
        if (_nodes.ContainsKey(position))
            return _nodes[position];

        return null;
    }
}
