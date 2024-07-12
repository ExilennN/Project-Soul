using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridtest : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform target;
    [SerializeField] private Transform origin;
    private Seeker seeker;
    private List<PathNode> path = new List<PathNode>();
    private void Start()
    {
        PathGrid grid = new PathGrid(20, 10, 5f, whatIsGround,new Vector3(-15,-12),true);
        seeker = new Seeker(grid);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = GetMouseWorldPos();
            seeker.GetGrid().GetXY(mousePos, out int x, out int y);
            List<PathNode> pathA = seeker.FindPath(new PathNode(0,1), new PathNode(x,y));
            if (pathA != null)
            {
                for (int i = 0; i < path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(pathA[i].X, pathA[i].Y) + new Vector3(0.5f, 0.5f), new Vector3(pathA[i + 1].X, pathA[i + 1].Y) + new Vector3(0.5f, 0.5f), Color.green, 10f);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        seeker.GetGrid().GetXY(target.position, out int xT, out int yT);
        seeker.GetGrid().GetXY(origin.position, out int xO, out int yO);
        List<PathNode> localPath = seeker.FindPath(new PathNode(xO, yO), new PathNode(xT, yT));
        if (localPath != null)
        {
            path = localPath;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].X, path[i].Y) + new Vector3(0.5f, 0.5f), new Vector3(path[i + 1].X, path[i + 1].Y) + new Vector3(0.5f, 0.5f), Color.red, 0.2f);
            }
        }
        else
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].X, path[i].Y) + new Vector3(0.5f, 0.5f), new Vector3(path[i + 1].X, path[i + 1].Y) + new Vector3(0.5f, 0.5f), Color.red, 0.2f);
            }
        }

    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(new Vector3(0.5f, 0.9f, 0), new Vector3(0.5f, 0.4f, 0));
    }

    public Vector3 GetMouseWorldPos()
    {
        Vector3 vec = GetMouseWorldPosWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    private Vector3 GetMouseWorldPosWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPos = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPos;
    }
}
