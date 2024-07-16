using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridController : MonoBehaviour
{
    
    public PathGrid pathGrid { get; private set; }

    [Header("Data grid file")]
    [FileName("json")] public string gridDataFile;

    [Header("Grid Data (if file not selected)")]
    public bool generateNewGrid;
    public int width;
    public float cellSize;
    public int height;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector3 originPosition;
    public bool showDebug;
    
    [Header("Grid File Save")]
    public string fileName;
    [Directory] public string directoryPath;
    public bool save;
    private void Awake()
    {
        if (gridDataFile == null || gridDataFile.Length == 0 || generateNewGrid) { pathGrid = new PathGrid(width, height, cellSize, whatIsGround, originPosition); }
        else { pathGrid = PathGrid.DowloadGrid(gridDataFile); } 
    }

    private void FixedUpdate()
    {
        if (save)
        {
            save = false;
            SaveGrid();
        }
        if (showDebug)
        {
            pathGrid.ShowDebug();
        }
        else
        {
            Utils.DeleteObjectsByTag("WorldTextDebug");
        }
    }

    public void SaveGrid()
    {
        if (fileName == null || fileName.Length == 0 || fileName.Contains("."))
        { 
            Debug.Log("Make sure \"Grid File Save\" not empty and fileName didnt have . in a name or type of file");
            return;
        }
        pathGrid.SaveGrid("./Assets/Enemies/Pathfinding/gridSaves", fileName+".json");
    }
}
