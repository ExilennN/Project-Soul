using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroudPathGrid : PathGrid
{
    public GroudPathGrid(int width, int height, float cellSize, LayerMask whatIsGround, Vector3 originPosition) : base(width, height, cellSize, whatIsGround, originPosition)
    {
        CalculateWalkableGrid();
        CalculateJumpEdges();
    }

    protected override void BuildBridgeBetweeenCells(PathNode start, PathNode end)
    {
        if (start.x != end.x) return;
        int currenty = start.y - 1;
        while (currenty != end.y)
        {
            wallkableCells.Add(new PathNode("A", start.x, currenty, false, true));
            currenty--;
        }
    }

    protected override void CalculateJumpEdges()
    {
        List<PathNode> temp = new List<PathNode>(wallkableCells);
        foreach (PathNode node in temp)
        {
            if (!wallkableCells.Contains(new PathNode(node.x - 1, node.y)))
            {
                if (!Physics2D.Raycast(GetCenterOfNode(GetWorldPosition(node.x - 1, node.y)), Vector2.up, cellSize, whatIsGround))
                {
                    bool isThereGroundUnder = false;
                    for (int i = 1; i < 5; i++)
                    {
                        if (wallkableCells.Contains(new PathNode(node.x - 1, node.y - i)))
                        {
                            isThereGroundUnder = true;
                            BuildBridgeBetweeenCells(new PathNode(node.x - 1, node.y), new PathNode(node.x - 1, node.y - i));
                            break;
                        }
                    }
                    if (isThereGroundUnder)
                    {
                        node.isEdge = true; node.nodeText = "E";
                        wallkableCells.Add(new PathNode("A", node.x - 1, node.y, false, true));
                    }
                }

            }
            if (!wallkableCells.Contains(new PathNode(node.x + 1, node.y)))
            {
                if (!Physics2D.Raycast(GetCenterOfNode(GetWorldPosition(node.x + 1, node.y)), Vector2.up, cellSize, whatIsGround))
                {
                    bool isThereGroundUnder = false;
                    for (int i = 1; i < 5; i++)
                    {
                        if (wallkableCells.Contains(new PathNode(node.x + 1, node.y - i)))
                        {
                            isThereGroundUnder = true;
                            BuildBridgeBetweeenCells(new PathNode(node.x + 1, node.y), new PathNode(node.x + 1, node.y - i));
                            break;
                        }
                    }
                    if (isThereGroundUnder)
                    {
                        node.isEdge = true; node.nodeText = "E";
                        wallkableCells.Add(new PathNode("A", node.x + 1, node.y, false, true));
                    }
                }

            }
        }
    }

    protected override void CalculateWalkableGrid()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (Physics2D.Raycast(GetCenterOfNode(GetWorldPosition(x, y)), Vector2.down, cellSize, whatIsGround)
                    && !Physics2D.Raycast(GetCenterOfNode(GetWorldPosition(x, y)), Vector2.up, cellSize, whatIsGround))
                {
                    wallkableCells.Add(new PathNode("W", x, y));
                }

            }
        }
    }
}
