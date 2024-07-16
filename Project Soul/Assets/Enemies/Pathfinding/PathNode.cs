using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathNode 
{
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;
    public string nodeText;
    public PathNode cameFrom;

    public bool isWalkable;
    public bool isAir;
    public bool isEdge;

    public PathNode(int x, int y, bool isWalkable = true, bool isAir = false, bool isEdge = false)
    {
        this.x = x; this.y = y;
        this.isWalkable = isWalkable;
        this.isAir = isAir;
        this.isEdge = isEdge;
    }
    public PathNode(string text, int x, int y, bool isWalkable = true, bool isAir = false, bool isEdge = false)
    {
        this.x = x; this.y = y; nodeText = text;
        this.isWalkable = isWalkable;
        this.isAir = isAir;
        this.isEdge = isEdge;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is PathNode)) return false;
        PathNode compare = (PathNode)obj;
        return compare.x == this.x && compare.y == this.y;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
