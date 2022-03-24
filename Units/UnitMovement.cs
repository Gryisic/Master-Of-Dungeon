using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement 
{
    private Transform _unit;
    private int _range;

    public int GetRange => _range;

    public UnitMovement(Transform unit)
    {
        _unit = unit;
    }

    public void Move(Vector2 direction, float delta) =>
        _unit.position = Vector2.MoveTowards(_unit.position, direction, delta);

    public IEnumerator MoveRoutine(List<SurfaceNode> path) 
    {
        var direction = MathExtension.Direction(path[0].GetPosition, path[1].GetPosition);
        var first = path[0].GetPosition;
        var last = new Vector2();

        for (int i = 0; i < path.Count; i++) 
        {
            if (i + 1 < path.Count)
            {
                if (MathExtension.Direction(path[i].GetPosition, path[i + 1].GetPosition) != direction) 
                {
                    last = path[i].GetPosition;

                    Move(path[i].GetPosition, HeuristicCost(first, last));

                    first = path[i].GetPosition;

                    direction = MathExtension.Direction(path[i].GetPosition, path[i + 1].GetPosition);

                    yield return new WaitForSeconds(0.2f);
                }
            }
            else 
            {
                last = path[i].GetPosition;

                Move(path[i].GetPosition, HeuristicCost(first, last));
            }
        }
    }

    private float HeuristicCost(Vector2 from, Vector2 to)
    {
        var distanceX = (int)Mathf.Abs(from.x - to.x);
        var distanceY = (int)Mathf.Abs(from.y - to.y);
        var remaining = Mathf.Abs(distanceX - distanceY);

        return 1.4f * Mathf.Min(distanceX, distanceY) + 1f * remaining;
    }
}
