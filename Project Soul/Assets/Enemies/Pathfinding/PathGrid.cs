using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Rendering.Universal.Internal;
[System.Serializable]
public class PathGrid
{
    public int width;
    public float cellSize;
    public int height;
    public int[,] gridArray;
    public List<PathNode> wallkableCells = new List<PathNode>();
    public LayerMask whatIsGround;
    public Vector3 originPosition;
    public PathGrid(int width, int height, float cellSize, LayerMask whatIsGround, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.whatIsGround = whatIsGround;
        this.originPosition = originPosition;

        gridArray = new int[width, height];

        //CalculateWalkableGrid();
        //CalculateJumpEdges();

    }
    public PathGrid() { }
    public void ShowDebug()
    {
        Utils.DeleteObjectsByTag("WorldTextDebug");
        foreach (PathNode node in wallkableCells)
        {
            Utils.CreateWorldText(null, node.nodeText, GetCenterOfNode(GetWorldPosition(node.x, node.y)), Color.white, new Vector3(0.3f,0.3f,0.3f) ,15, TextAnchor.MiddleCenter);
            Debug.DrawLine(GetWorldPosition(node.x, node.y), GetWorldPosition(node.x, node.y + 1), Color.white);
            Debug.DrawLine(GetWorldPosition(node.x, node.y), GetWorldPosition(node.x + 1, node.y), Color.white);
            Debug.DrawLine(GetWorldPosition(node.x, node.y + 1), GetWorldPosition(node.x + 1, node.y + 1), Color.white);
            Debug.DrawLine(GetWorldPosition(node.x + 1, node.y), GetWorldPosition(node.x + 1, node.y + 1), Color.white);
        }
    }
    protected virtual void CalculateWalkableGrid()
    {

    }
    protected virtual void CalculateJumpEdges()
    {
        
    }
    protected virtual void BuildBridgeBetweeenCells(PathNode start, PathNode end)
    {
        
    }
    public List<PathNode> GetWalkableList() { return wallkableCells; }

    public void SaveGrid(string path, string fileName)
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(path + "\\" + fileName, json);
    }
    static public PathGrid DowloadGrid(string path)
    {
        return JsonUtility.FromJson<PathGrid>(File.ReadAllText("./"+path));
    }

    public Vector3 GetCenterOfNode(PathNode node)
    {
        return new Vector3(node.x, node.y) + new Vector3(cellSize, cellSize) * .5f;
    }
    public Vector3 GetCenterOfNode(int x, int y)
    {
        return new Vector3(x, y) + new Vector3(cellSize, cellSize) * .5f;
    }
    public Vector3 GetCenterOfNode(Vector3 nodePosition)
    {
        return nodePosition + new Vector3(cellSize, cellSize) * .5f;
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition ;
    }
    public void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPos - originPosition).y / cellSize);
    }

}
