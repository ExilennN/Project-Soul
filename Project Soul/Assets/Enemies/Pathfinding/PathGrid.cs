using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Rendering.Universal.Internal;


public class PathGrid : MonoBehaviour
{
    private int width;
    private float cellSize;
    private int height;
    private int[,] gridArray;
    private List<PathNode> wallkableCells = new List<PathNode>();
    private LayerMask whatIsGround;
    private Vector3 originPosition;
    private bool showDebug;
    public PathGrid(int width, int height, float cellSize, LayerMask whatIsGround, Vector3 originPosition, bool showDebug = false)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.whatIsGround = whatIsGround;
        this.originPosition = originPosition;

        gridArray = new int[width, height];

        CalculateWalkableGrid();

        //CalculateJumpEdges();
        if (showDebug)
        {
            foreach (PathNode node in wallkableCells)
            {
                CreateWorldText(null, node.nodeText, GetWorldPosition(node.X, node.Y) + new Vector3(cellSize, cellSize) * .5f, Color.white, 40, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(node.X, node.Y), GetWorldPosition(node.X, node.Y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(node.X, node.Y), GetWorldPosition(node.X + 1, node.Y), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(node.X, node.Y + 1), GetWorldPosition(node.X + 1, node.Y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(node.X + 1, node.Y), GetWorldPosition(node.X + 1, node.Y + 1), Color.white, 100f);
            }
        }
    }

    private void CalculateJumpEdges()
    {
        List<PathNode> temp = new List<PathNode>(wallkableCells);
        foreach (PathNode node in temp)
        {
            if (!wallkableCells.Contains(new PathNode(node.X-1, node.Y)))
            {
                if (!Physics2D.Raycast(GetWorldPosition(node.X - 1, node.Y) + new Vector3(0.5f, 0.5f), Vector2.up, 1f, whatIsGround))
                {
                    bool isThereGroundUnder = false;
                    for (int i = 1; i < 5; i++)
                    {
                        if (wallkableCells.Contains(new PathNode(node.X - 1, node.Y - i))) { 
                            isThereGroundUnder = true;
                            BuildBridgeBetweeenCells(new PathNode(node.X - 1, node.Y), new PathNode(node.X - 1, node.Y - i));
                            break; 
                        }
                    }
                    if (isThereGroundUnder) 
                    {
                        node.isEdge = true; node.nodeText = "E";
                        wallkableCells.Add(new PathNode("A",node.X - 1, node.Y, false, true)); 
                    }
                }
                    
            }
            if (!wallkableCells.Contains(new PathNode(node.X+1, node.Y)))
            {
                if (!Physics2D.Raycast(GetWorldPosition(node.X + 1, node.Y) + new Vector3(0.5f, 0.5f), Vector2.up, 1f, whatIsGround))
                {
                    bool isThereGroundUnder = false;
                    for (int i = 1; i < 5; i++)
                    {
                        if (wallkableCells.Contains(new PathNode(node.X + 1, node.Y - i))) { 
                            isThereGroundUnder = true;
                            BuildBridgeBetweeenCells(new PathNode(node.X + 1, node.Y), new PathNode(node.X + 1, node.Y - i));
                            break; 
                        }
                    }
                    if (isThereGroundUnder) 
                    {
                        node.isEdge = true; node.nodeText = "E";
                        wallkableCells.Add(new PathNode("A", node.X + 1, node.Y, false, true)); 
                    }
                }
                
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition ;
    }

    public void CalculateWalkableGrid()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //if (Physics2D.Raycast(GetWorldPosition(x, y) + new Vector3(0.5f, 0.5f), Vector2.down, 1f, whatIsGround)
                //    && !Physics2D.Raycast(GetWorldPosition(x, y) + new Vector3(0.5f, 0.5f), Vector2.up, 1f, whatIsGround))
                //{
                    wallkableCells.Add(new PathNode("W",x, y));
                //}

            }
        }          
    }
    public void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPos - originPosition).y / cellSize);
    }
    private void BuildBridgeBetweeenCells(PathNode start, PathNode end) {
        if (start.X != end.X) return;
        int currentY = start.Y-1;
        while (currentY != end.Y)
        {
            wallkableCells.Add(new PathNode("A", start.X, currentY, false, true));
            currentY--;
        }
    }

    private static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, Color color, int fontSize = 40, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000 )
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.color = color;
        textMesh.alignment = textAlignment;
        textMesh.fontSize = fontSize;
        textMesh.text = text;
        textMesh.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }


    public List<PathNode> GetWalkableList() { return wallkableCells; }
}
