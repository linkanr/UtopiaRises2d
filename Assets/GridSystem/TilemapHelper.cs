using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System;

public static class TilemapHelper
{
    private static readonly int[] tileOrder = { 5, 15, 3, 9, 11, 6, 13, 10, 7, 14, 4, 8, 1, 2, 0, 12 };
    private static readonly int[] tileOrder35 = { 6, 3, 4,11, 8, 7, 13, 12, 9, 14,5,10, 1,2, 0, 15 };
    public static void SortNewList(List<TileBase> sourceTiles, List<TileBase> targetTiles)
    {
        foreach (int i in tileOrder35)
        {
            targetTiles.Add(sourceTiles != null && i < sourceTiles.Count ? sourceTiles[i] : null);
        }
    }

    public static IEnumerator UpdateTileCoroutine(Cell cell, Tilemap tilemap, List<TileBase> sortedTileBaseList, int extraTiles = 0)
    {
        yield return null;

        Vector3[] cornerPositions = GetCornerPositions(cell.worldPosition, cell.size);
        Vector3Int[] cellPositions = Array.ConvertAll(cornerPositions, tilemap.WorldToCell);
        

        for (int i = 0; i < cellPositions.Length; i++)
        {
            List<Cell> cellList = cell.gridRef.GetTileCells(cornerPositions[i]);
            if (cellList == null) continue;

            int rand = UnityEngine.Random.Range(0, 2) * 16 * extraTiles;
            int index = CalculateTileIndex(cellList, rand);

            TileBase tile = sortedTileBaseList[index];
            if (tile != null)
            {
                
                tilemap.SetTile(cellPositions[i], tile);
            }
        }
    }

    public static IEnumerator UpdateTileWithNoise(Cell cell, Tilemap tilemap, List<TileBase> sortedTileBaseList, float freq)
    {
        yield return null;

        Vector3[] cornerPositions = GetCornerPositions(cell.worldPosition, cell.size);
        Vector3Int[] cellPositions = Array.ConvertAll(cornerPositions, tilemap.WorldToCell);

        for (int i = 0; i < cellPositions.Length; i++)
        {
            List<Cell> cellList = cell.gridRef.GetTileCells(cornerPositions[i]);
            if (cellList == null) continue;

            // Calculate the tile index using the maximum of binaryInList and Perlin noise
            int index = CalculateTileIndexWithMaxNoise(cellList, freq);
            TileBase tile = sortedTileBaseList[index];

            if (tile != null)
            {
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

    private static int CalculateTileIndex(List<Cell> cellList, int rand)
    {
        return cellList[0].cellTerrain.binaryInList * 8 +
               cellList[1].cellTerrain.binaryInList * 4 +
               cellList[2].cellTerrain.binaryInList * 2 +
               cellList[3].cellTerrain.binaryInList + rand;
    }

    private static int CalculateTileIndexWithMaxNoise(List<Cell> cellList, float freq)
    {
        return GetValue(cellList[0].cellTerrain.binaryInList, cellList[0].worldPosition, freq) * 8 +
               GetValue(cellList[1].cellTerrain.binaryInList, cellList[1].worldPosition, freq) * 4 +
               GetValue(cellList[2].cellTerrain.binaryInList, cellList[2].worldPosition, freq) * 2 +
               GetValue(cellList[3].cellTerrain.binaryInList, cellList[3].worldPosition, freq);

        static int GetValue(int binary, Vector3 position, float freq)
        {
            float noiseValue = Mathf.PerlinNoise(position.x * freq, position.y * freq);
            return Mathf.Max(binary, noiseValue > 0.5f ? 1 : 0); // Take max of binary and noise (threshold for noise is 0.5)
        }
    }
}
