using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellManager : SerializedMonoBehaviour
{
    public static GridCellManager instance;
    public GridConstrution gridConstrution;

    public float gridSize;
    public Dictionary<CellTerrainEnum, CellTerrain > cellTerrainDic;
    public Transform BG;
    GridGraph gridGraph;
    CellSolver cellSolver;

    private void Awake()
    {
        instance = this;
        
        
        

    }
    private void Update()
    {


    }
    public IEnumerator InitCellSolver()
    {
        cellSolver = gameObject.AddComponent<CellSolver>();
        cellSolver.Init();
        yield return null;

    }

    public IEnumerator GenerateGrid()
    {
        //gridConstrution = new GridConstrution(gridAmountX, gridAmountY, gridSize, new Vector3(0f, 0f, 0f), cellTerrainList,xmulti,ymulti);
        
        gridConstrution = new GridConstrution(GameManager.instance.currentLevel.map, gridSize,new Vector3(0f,0f,0f));
        Vector3 offset = new Vector3(gridConstrution.sizeX * gridSize / 2 , gridConstrution.sizeY * gridSize / 2, 0.01f);
        BG.position = offset;



        CellActions.UpdateCells(gridConstrution.GetCellList(2));
        Debug.Log("Grid Generated");
        yield return null;

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