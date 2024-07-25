using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlyPathGrid : PathGrid
{
    public FlyPathGrid(int width, int height, float cellSize, LayerMask whatIsGround, Vector3 originPosition) : base(width, height, cellSize, whatIsGround, originPosition)
    {
        CalculateWalkableGrid();
    }

    protected override void BuildBridgeBetweeenCells(PathNode start, PathNode end)
    {
        base.BuildBridgeBetweeenCells(start, end);
    }

    protected override void CalculateJumpEdges()
    {
        base.CalculateJumpEdges();
    }

    protected override void CalculateWalkableGrid()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (!Physics2D.Raycast(GetCenterOfNode(GetWorldPosition(x, y)), Vector2.down, cellSize, whatIsGround)
                    || !Physics2D.Raycast(GetCenterOfNode(GetWorldPosition(x, y)), Vector2.up, cellSize, whatIsGround))
                {
                    wallkableCells.Add(new PathNode("F", x, y));
                }

            }
        }
    }
}
