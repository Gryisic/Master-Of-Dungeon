using System.Collections.Generic;
using UnityEngine;

public class Pathfinding 
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private List<SurfaceNode> _reachableNodes = new List<SurfaceNode>();
    private HashSet<SurfaceNode> _visitedNodes = new HashSet<SurfaceNode>();

    public List<SurfaceNode> Path(Vector2 from, Vector2 to, SurfaceGrid grid)
    {
        from = MathExtension.GetCentralPosition(from);
        to = MathExtension.GetCentralPosition(to);

        var initialNode = grid.GetNodes.ContainsKey(from) ? grid.GetNodes[from] : null;
        var finalNode = grid.GetNodes.ContainsKey(to) ? grid.GetNodes[to] : null;

        _reachableNodes.Clear();
        _visitedNodes.Clear();

        _reachableNodes.Add(initialNode);

        if (initialNode != null && finalNode != null)
        {
            initialNode.SetCost(0, HeuristicCost(initialNode, finalNode));

            while (_reachableNodes.Count > 0)
            {
                SurfaceNode currentNode = GetNodeWithLowerFinalCost(_reachableNodes);

                if (currentNode == finalNode) return BuildedPath(finalNode, initialNode);

                _reachableNodes.Remove(currentNode);
                _visitedNodes.Add(currentNode);

                foreach (var neighbourNode in grid.GetNeighbours(currentNode))
                {
                    if (neighbourNode == null || _visitedNodes.Add(neighbourNode) == false)
                        continue;

                    var tentativeCost = currentNode.GetGeneralCost + HeuristicCost(currentNode, neighbourNode);

                    if (tentativeCost < neighbourNode.GetGeneralCost) 
                    {
                        neighbourNode.SetParentNode(currentNode);
                        neighbourNode.SetCost(currentNode.GetGeneralCost + HeuristicCost(currentNode, neighbourNode),
                            HeuristicCost(neighbourNode, finalNode));

                        if (_reachableNodes.Contains(neighbourNode) == false)
                            _reachableNodes.Add(neighbourNode);
                    }
                }
            }
        }

        return null;
    }

    private List<SurfaceNode> BuildedPath(SurfaceNode finalNode, SurfaceNode initialNode) 
    {
        var path = new List<SurfaceNode>();
        var currentNode = finalNode;

        path.Add(currentNode);

        if (initialNode == finalNode) return null;

        while(currentNode.ParentNode != null) 
        {
            if (path.Contains(currentNode.ParentNode) == false)
            {
                path.Add(currentNode.ParentNode);

                currentNode = currentNode.ParentNode;
            }
        }

        path.Reverse();

        return path;
    }

    private SurfaceNode GetNodeWithLowerFinalCost(List<SurfaceNode> nodes) 
    {
        var lowerCost = int.MaxValue;

        SurfaceNode cheapestNode = null;

        foreach (var node in nodes) 
        {
            if (node.GetFinalCost < lowerCost)
            {
                lowerCost = node.GetFinalCost;
                cheapestNode = node;
            }
        }

        return cheapestNode;
    }

    private int HeuristicCost(SurfaceNode from, SurfaceNode to) 
    {
        var distanceX = (int)Mathf.Abs(from.GetPosition.x - to.GetPosition.x);
        var distanceY = (int)Mathf.Abs(from.GetPosition.y - to.GetPosition.y);
        var remaining = Mathf.Abs(distanceX - distanceY);

        return MOVE_DIAGONAL_COST * Mathf.Min(distanceX, distanceY) + MOVE_STRAIGHT_COST * remaining;
    }
}
