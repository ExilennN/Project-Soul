using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent
{
    public List<PathNode> path { get; private set; }
    public PathNode currentNode { get; set; }
    private int currentNodeIndex;
    public bool isPathFinished;
    public PathAgent(List<PathNode> path)
    {
        currentNodeIndex = 0;
        this.path = path;
        isPathFinished = false;
        currentNode = path[currentNodeIndex];
    }

    public PathNode GetNode()
    {
        currentNodeIndex += currentNodeIndex >= path.Count ? 0 : 1;
        SetIsPathFinished();
        currentNode = path[currentNodeIndex-1];
        return currentNode;
    }
    public PathNode GetNode(int index)
    {
        return index+currentNodeIndex >= path.Count ? path[path.Count-1] : path[currentNodeIndex+index];
    }
    public PathNode GetNextNode() { return  currentNodeIndex+1 >= path.Count ? path[path.Count - 1] : path[currentNodeIndex]; }
    private void SetIsPathFinished()
    {
        isPathFinished = currentNodeIndex >= path.Count ? true : false;
    }
}
