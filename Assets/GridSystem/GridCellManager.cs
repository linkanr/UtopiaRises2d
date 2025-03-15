using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellManager : SerializedMonoBehaviour
{
    public static GridCellManager Instance;
    public GridConstrution gridConstrution;
    public Texture2D texture;
    public float gridSize;
    public Dictionary<CellTerrainEnum, CellTerrain > cellTerrainDic;
    public Transform BG;
    GridGraph gridGraph;
    CellSolver cellSolver;

    private void Awake()
    {
        Instance = this;
        GenereateGrid();
        cellSolver = gameObject.AddComponent<CellSolver>();
        

    }
    private void Start()
    {
        
        cellSolver.Init();
    }

    private void GenereateGrid()
    {
        //gridConstrution = new GridConstrution(gridAmountX, gridAmountY, gridSize, new Vector3(0f, 0f, 0f), cellTerrainList,xmulti,ymulti);
        
        gridConstrution = new GridConstrution(texture, gridSize,new Vector3(0f,0f,0f));
        Vector3 offset = new Vector3(gridConstrution.sizeX * gridSize / 2 , gridConstrution.sizeY * gridSize / 2, 0);
        BG.position = offset;


        CellActions.UpdateCells(gridConstrution.GetCellList(2));


    }
    public CellTerrain GetTerrainFromEneum(CellTerrainEnum cellTerrainEnum)
    {
        return cellTerrainDic[cellTerrainEnum];
    }
    private void OnDestroy()
    {
        foreach (Cell cell in gridConstrution.GetCellList())
        {
            cell.Dispose();
        }
    }
}