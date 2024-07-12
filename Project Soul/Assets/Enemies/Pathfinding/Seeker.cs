using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker
{
    private const int MOVE_COST = 1;
    private PathGrid walkableGrid;
    private List<PathNode> walkableCells;
    private List<PathNode> openList;
    private List<PathNode> closedList;
    public Seeker(PathGrid walkableGrid){
        this.walkableGrid = walkableGrid;

        walkableCells = this.walkableGrid.GetWalkableList();
    }

    public PathGrid GetGrid() { return walkableGrid; }

    public List<PathNode> FindPath(PathNode start, PathNode end)
    {

        openList = new List<PathNode>() { start };
        closedList = new List<PathNode>();

        foreach (PathNode node in walkableCells)
        {
            node.gCost = int.MaxValue;
            node.CalculateFCost();
            node.cameFrom = null;
        }

        start.gCost = 0;
        start.hCost = CalculateDistance(start, end);
        start.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode.X == end.X && currentNode.Y == end.Y)
            {
                return CalculatePath(currentNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            foreach (PathNode node in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(node)) { continue; }
                int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, node);
                if (tentativeGCost < node.gCost)
                {
                    node.cameFrom = currentNode;
                    node.gCost = tentativeGCost;
                    node.hCost = CalculateDistance(node, end);
                    node.CalculateFCost();

                    if (!openList.Contains(node)) { openList.Add(node); }
                }
            }
        }
        // Out of nodes in opeenList
        return null;
    }
    private List<PathNode> GetNeighbourList(PathNode originNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        //Left
        if (walkableCells.Contains(new PathNode(originNode.X - 1, originNode.Y))) { neighbourList.Add(walkableCells.Find(elem=>elem.X == originNode.X - 1 && elem.Y == originNode.Y)); }
        //Right
        if (walkableCells.Contains(new PathNode(originNode.X + 1, originNode.Y))) { neighbourList.Add(walkableCells.Find(elem => elem.X == originNode.X + 1 && elem.Y == originNode.Y)); }
        //Down
        if (walkableCells.Contains(new PathNode(originNode.X, originNode.Y - 1))) { neighbourList.Add(walkableCells.Find(elem => elem.X == originNode.X && elem.Y == originNode.Y - 1)); }
        //Up
        if (walkableCells.Contains(new PathNode(originNode.X, originNode.Y + 1))) { neighbourList.Add(walkableCells.Find(elem => elem.X == originNode.X && elem.Y == originNode.Y + 1)); }

        return neighbourList;
    }

    private List<PathNode> CalculatePath (PathNode end)
    {
        List<PathNode> path = new List<PathNode>() { end };
        PathNode currentNode = end;
        while (currentNode.cameFrom != null)
        {
            path.Add(currentNode.cameFrom);
            currentNode = currentNode.cameFrom;
        }
        path.Reverse();
        return path;

    }
    public int CalculateDistance(PathNode start, PathNode end)
    {
        int xDistance = Mathf.Abs(start.X - end.X);
        int yDistance = Mathf.Abs(start.Y - end.Y);
        int remainig = Mathf.Abs(xDistance - yDistance);
        return Mathf.Min(xDistance, yDistance) + MOVE_COST * remainig;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        foreach (PathNode node in pathNodeList)
        {
            if (node.fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = node;
            }
        }
        return lowestFCostNode;
    }
}
