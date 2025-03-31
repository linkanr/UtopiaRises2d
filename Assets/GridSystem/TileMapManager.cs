using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Collections;
using System;

public class TilemapManager : SerializedMonoBehaviour
{
    public Tilemap playerTerrainMap;
    public Tilemap enemySoilTileMAp;
    public Tilemap enemyGrassTileMap;
    public Tilemap enemyWaterTileMap;
    public Tilemap cellEffectTileMap;

    private List<TileBase> UvTileRerrain;
    public List<TileBase> UvTilesSorted;
    public List<TileBase> enemyTerrainSoil;
    private List<TileBase> enemyTerrainSoilSorted;
    public List<TileBase> enemyTerraingrass;
    private List<TileBase> enemyTerraingrassSorted;
    public List<TileBase> cellEffectTiles;




    private void Awake()
    {
        UvTileRerrain = new List<TileBase>();
        enemyTerrainSoilSorted = new List<TileBase>();
        enemyTerraingrassSorted = new List<TileBase>();


        TilemapHelper.SortNewList(UvTilesSorted, UvTileRerrain);
        //TilemapHelper.SortNewList(baseTilesAlt, baseTilesSorted);
        TilemapHelper.SortNewList(enemyTerrainSoil, enemyTerrainSoilSorted);
        TilemapHelper.SortNewList(enemyTerraingrass, enemyTerraingrassSorted);
    
    }

    private void OnEnable()
    {
        CellActions.UpdateCells += UpdateCellList;
        CellActions.UpdateCellEffect += SetCellEffect;
    }

    private void OnDisable()
    {
        CellActions.UpdateCells -= UpdateCellList;
        CellActions.UpdateCellEffect -= SetCellEffect;
    }



    private void UpdateCellList(List<Cell> cells)
    {
        foreach (Cell cell in cells)
        {
   //         Debug.Log("Updating cell");
            UpdateTile(cell);
        }
    }

    public void UpdateTile(Cell cell)
    {
        StartCoroutine(UpdateTileAndTriggerAstar(cell));
    }

    private IEnumerator UpdateTileAndTriggerAstar(Cell cell)
    {
        // Run the first two coroutines and wait for them to complete
       yield return UpdatePlayerTerrain(cell);
        yield return UpdateEnemyTerrain(cell); 

       

        // After the first two coroutines are done, trigger the A* update

    }

    public IEnumerator UpdatePlayerTerrain(Cell _cell)
    {
        yield return StartCoroutine(TilemapHelper.UpdateTileCoroutine(_cell, playerTerrainMap, UvTileRerrain, CellTerrainEnum.playerTerrain));

      
    }
    public IEnumerator UpdateEnemyTerrain(Cell _cell)
    {

        yield return StartCoroutine(TilemapHelper.UpdateTileCoroutine(_cell, enemySoilTileMAp, enemyTerrainSoilSorted, CellTerrainEnum.soil));
        yield return StartCoroutine(TilemapHelper.UpdateTileCoroutine(_cell, enemyGrassTileMap, enemyTerraingrassSorted, CellTerrainEnum.grass));
        yield return StartCoroutine(TilemapHelper.UpdateTileCoroutine(_cell, enemyWaterTileMap, UvTileRerrain, CellTerrainEnum.water));
    }

    /// <summary>
    /// Gets triggered when the cell action is called
    /// </summary>
    /// <param name="args"></param>
    public void SetCellEffect(CellEffectUpdateArgs args)
    {
        if (args.cellEffect == CellEffectEnum.None)
        {
            Vector3Int cellPosition = cellEffectTileMap.WorldToCell(args.cell.worldPosition);
            cellEffectTileMap.SetTile(cellPosition, null); // Removes the tile
            return; // Exit early since there's no effect to apply
        }

        TileBase tile = CellEffectUpdater.GetCellEffectTile(args.cellEffect, cellEffectTiles);
        if (tile != null)
        {
            Vector3Int cellPosition = cellEffectTileMap.WorldToCell(args.cell.worldPosition);
            cellEffectTileMap.SetTile(cellPosition, tile);
        }
    }



}
public class CellEffectUpdater
{

    public static TileBase GetCellEffectTile(CellEffectEnum cellEffect, List<TileBase> tileBases)
    {
        switch (cellEffect)
        {
            case CellEffectEnum.Fire:
                return tileBases[0];

            case CellEffectEnum.Gas:
                return tileBases[1];
            default:
                return null;
        }

    }
}
