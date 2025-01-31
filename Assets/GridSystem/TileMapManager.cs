using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Collections;
using System;

public class TilemapManager : SerializedMonoBehaviour
{
    public Tilemap baseTilesTileMap;
    public Tilemap topTilesTileMap;
    public Tilemap cellEffectTileMap;

    private List<TileBase> baseTilesSorted;
    public List<TileBase> baseTiles;
    public List<TileBase> topTiles;
    private List<TileBase> topTilesSorted;
    public List<TileBase> cellEffectTiles;


    private void Awake()
    {
        baseTilesSorted = new List<TileBase>();
        topTilesSorted = new List<TileBase>();

        TilemapHelper.SortNewList(baseTiles, baseTilesSorted);
        //TilemapHelper.SortNewList(baseTilesAlt, baseTilesSorted);
        TilemapHelper.SortNewList(topTiles, topTilesSorted);
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
        yield return StartCoroutine(TilemapHelper.UpdateTileCoroutine(cell, baseTilesTileMap, baseTilesSorted, 0));
       yield return StartCoroutine(TilemapHelper.UpdateTileWithNoise(cell, topTilesTileMap, topTilesSorted, 0.255f));

        // After the first two coroutines are done, trigger the A* update

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
