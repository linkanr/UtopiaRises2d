using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System;

public static class TilemapHelper
{
    //6, 3, 4,11, 8, 7, 13, 12, 9, 14,5,10, 1,2, 0, 15
    private static readonly int[] tileOrder35 = { 15, 0, 2, 1, 10, 5, 14, 9, 12, 13, 7, 8, 11, 4, 3, 6 };
    public static void SortNewList(List<TileBase> sourceTiles, List<TileBase> targetTiles)
    {
        foreach (int i in tileOrder35)
        {
            targetTiles.Add(sourceTiles != null && i < sourceTiles.Count ? sourceTiles[i] : null);
        }
    }

    public static IEnumerator UpdateTileCoroutine(Cell cell, Tilemap tilemap, List<TileBase> sortedTileBaseList, CellTerrainEnum terrain)
    {
        yield return null;
        //Debug.Log("UpdateTileCoroutine");
        Vector3[] cornerPositions = GetCornerPositions(cell.worldPosition, cell.size);
        Vector3Int[] cellPositions = Array.ConvertAll(cornerPositions, tilemap.WorldToCell);
        

        for (int i = 0; i < cellPositions.Length; i++)
        {
            List<Cell> cellList = cell.gridRef.GetTileCells(cornerPositions[i]);
            if (cellList == null) continue;


            int index = CalculateTileIndex(cellList, terrain);

           //Debug.Log("new tile Index: " + index);
            TileBase tile = sortedTileBaseList[index];
            if (tile != null)
            {
               // Debug.Log("Setting tile");
                tilemap.SetTile(cellPositions[i], tile);
            }
        }
    }



    private static Vector3[] GetCornerPositions(Vector3 worldPosition, float cellSize)
    {
        float halfSize = cellSize / 2;
        return new Vector3[]
        {
            new Vector3(worldPosition.x - halfSize, worldPosition.y + halfSize, 0f),
            new Vector3(worldPosition.x + halfSize, worldPosition.y + halfSize, 0f),
            new Vector3(worldPosition.x - halfSize, worldPosition.y - halfSize, 0f),
            new Vector3(worldPosition.x + halfSize, worldPosition.y - halfSize, 0f)
        };
    }
    private static int CalculateTileIndex(List<Cell> cellList,CellTerrainEnum cellTerrainEnum)
    {
        int index = 0;
        int i = 0;
        int[] multipliers = { 8, 4, 2, 1 };
        foreach (Cell cell in cellList)
        {
            if (cell.cellTerrain.cellTerrainEnum == cellTerrainEnum)
            {
                index += multipliers[i];
            }
            i++;
        }
        return index;
    }



}
