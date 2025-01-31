using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellManager : MonoBehaviour
{
    public static GridCellManager Instance;
    public GridConstrution gridConstrution;
    public Texture2D texture;
    public float gridSize;
    [SerializeField] private float xmulti;
    [SerializeField] private float ymulti;
    public List<CellTerrain> cellTerrainList;
    public Transform BG;
    GridGraph gridGraph;

    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {
        GenereateGrid();
    }

    private void GenereateGrid()
    {
        //gridConstrution = new GridConstrution(gridAmountX, gridAmountY, gridSize, new Vector3(0f, 0f, 0f), cellTerrainList,xmulti,ymulti);
        
        gridConstrution = new GridConstrution(texture, cellTerrainList, gridSize,new Vector3(0f,0f,0f));
        Vector3 offset = new Vector3(gridConstrution.sizeX * gridSize / 2 , gridConstrution.sizeY * gridSize / 2, 0);
        BG.position = offset;


        CellActions.UpdateCells(gridConstrution.GetCellList(2));


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GenereateGrid();
        }
    }
}