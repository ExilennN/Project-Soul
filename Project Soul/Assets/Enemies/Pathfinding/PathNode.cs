using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    public int X { get; set; }
    public int Y { get; set; }

    public int gCost;
    public int hCost;
    public int fCost;
    public string nodeText { get; set; }
    public PathNode cameFrom;

    public bool isWalkable;
    public bool isAir;
    public bool isEdge;

    public PathNode(int x, int y, bool isWalkable = true, bool isAir = false, bool isEdge = false)
    {
        X = x; Y = y;
        this.isWalkable = isWalkable;
        this.isAir = isAir;
        this.isEdge = isEdge;
    }
    public PathNode(string text, int x, int y, bool isWalkable = true, bool isAir = false, bool isEdge = false)
    {
        X = x; Y = y; nodeText = text;
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
        return compare.X == X && compare.Y == Y;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
