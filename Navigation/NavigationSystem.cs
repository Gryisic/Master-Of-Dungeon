using System.Collections.Generic;
using UnityEngine;

public class NavigationSystem : MonoBehaviour 
{
    [SerializeField] private Room _currentRoom;

    private SurfaceGrid _grid;
    private Pathfinding _pathfinding = new Pathfinding();
    [SerializeField]private Unit _activeUnit;
    private List<Unit> _units;
    private List<SurfaceNode> _testPath = new List<SurfaceNode>();
    private CustomCoroutine<List<SurfaceNode>> _routine;

    private void Awake()
    {
        GenerateSurfaceGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            _routine = new CustomCoroutine<List<SurfaceNode>>(this, _activeUnit.Movement.MoveRoutine);

            _testPath?.Clear();
            _testPath = _pathfinding.Path(_activeUnit.GetPosition, DestinationSelector.Destination(), _grid);

            if (_testPath != null)
                Move(_testPath);
        }
    }

    public void GenerateSurfaceGrid()
    {
        _grid = new SurfaceGrid();

        var from = new Vector2(_currentRoom.GetFloor.origin.x,
            _currentRoom.GetFloor.origin.y);
        var to = from + new Vector2(_currentRoom.GetFloor.size.x - 1,
            _currentRoom.GetFloor.size.y - 1);

        _grid.Build(from, to, _currentRoom.GetFloor);
    }

    private void Move(List<SurfaceNode> path) 
    {
        _routine.Start(path);

        _grid.ClearNodes();
    }

    private void OnDrawGizmos()
    {
        if (_grid != null) 
        {
            foreach (var node in _grid.GetNodes) 
            {
                Gizmos.color = Color.white;

                var position1 = new Vector2(node.Key.x - 0.5f, node.Key.y - 0.5f);
                var position2 = new Vector2(position1.x + 1f, position1.y + 1f);

                Gizmos.DrawLine(position1, new Vector2(position1.x + 1f, position1.y));
                Gizmos.DrawLine(position1, new Vector2(position1.x, position1.y + 1f));
                Gizmos.DrawLine(position2, new Vector2(position2.x - 1f, position2.y));
                Gizmos.DrawLine(position2, new Vector2(position2.x, position2.y - 1f));
            }

            if (_testPath != null && _testPath.Count > 0)
            {
                Gizmos.color = Color.blue;

                for (int i = 0; i < _testPath.Count; i++)
                {
                    if (i + 1 < _testPath.Count)
                        Gizmos.DrawLine(_testPath[i].GetPosition, _testPath[i + 1].GetPosition);
                }
            }
        }
    }
}
