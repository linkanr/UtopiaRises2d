using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Collections;
using System;

public class TilemapManager : SerializedMonoBehaviour
{
    
    public Tilemap baseTilesTileMap;
    public Tilemap topTilesTileMap;

    private List<TileBase> baseTilesSorted;
    public List<TileBase> baseTiles;
    public List<TileBase> baseTilesAlt;
    public List<TileBase> topTiles;
    private List<TileBase> topTilesSorted;
    private void Awake()
    {

        baseTilesSorted = new List<TileBase>();
        topTilesSorted = new List<TileBase>();
        SortNewList(baseTiles, baseTilesSorted);
        SortNewList(baseTilesAlt, baseTilesSorted);
        SortNewList(topTiles, topTilesSorted);
    }

    private void SortNewList(List<TileBase> newTiles, List<TileBase> tiles)
    {
        int[] tileOrder = new int[] { 5, 15, 3,9,11,6, 14,10,  7, 14, 4,8,1,2,0,12};
        foreach (int i in tileOrder)

        {
            Debug.Log(i);

              

            if (newTiles.Count < i-1)
            {
                Debug.Log("new tiles count are " + newTiles.Count + " are less than " + i +  " minus 1");
                tiles.Add(null);
            }
            else
            {
                tiles.Add(newTiles[i]);
            }
            
        }

    }

    private void OnEnable()
    {
        CellActions.UpdateCells += UpdateCellList;
    }
    private void OnDisable()
    {
        CellActions.UpdateCells -= UpdateCellList;
    }

    private void UpdateCellList(List<Cell> list)
    {
        Debug.Log("UpdateCellList");    
        foreach (Cell cell in list)
        {
            //Debug.Log("UpdateCellList" + cell);
            UpdateTile(cell);
        }
    }

    public void SetTile(int x, int y, TileBase tile)
    {
        Debug.Log("Setting tile at " + x + ", " + y);
        Vector3Int position = new Vector3Int(x, y, 0);

        // Set the tile
        baseTilesTileMap.SetTile(position, tile);

        // Refresh to ensure changes are applied
        baseTilesTileMap.RefreshAllTiles();

        // Retrieve the tile back to confirm
        TileBase retrievedTile = baseTilesTileMap.GetTile(position);
        if (retrievedTile == null)
        {
            Debug.LogError($"Tile is null in new tilegrid at ({x}, {y}).");
        }
        else
        {
            Debug.Log($"Tile successfully set at ({x}, {y}).");
        }
    }

    public void UpdateTile(Cell cell)
    {
        StartCoroutine(UpdateTileCoroutine(cell, baseTilesTileMap,baseTilesSorted,1));
        StartCoroutine(UpdateTileFromNoise(cell, topTilesTileMap, topTilesSorted));
    }

    private IEnumerator UpdateTileCoroutine(Cell cell,Tilemap tilemap, List<TileBase> sortedTileBaseList, int extraTiles = 0)
    {
        yield return null;

        Vector3 worldPosition = cell.worldPosition;
        float cellSize = cell.size;

        Vector3 pos1 = new Vector3(worldPosition.x - cellSize / 2, worldPosition.y + cellSize / 2, 0f);
        Vector3 pos2 = new Vector3(worldPosition.x + cellSize / 2, worldPosition.y + cellSize / 2, 0f);
        Vector3 pos3 = new Vector3(worldPosition.x - cellSize / 2, worldPosition.y - cellSize / 2, 0f);
        Vector3 pos4 = new Vector3(worldPosition.x + cellSize / 2, worldPosition.y - cellSize / 2, 0f);

        Vector3[] worldPositions = { pos1, pos2, pos3, pos4 };
        Vector3Int[] cellPositions = { tilemap.WorldToCell(pos1), tilemap.WorldToCell(pos2), tilemap.WorldToCell(pos3), tilemap.WorldToCell(pos4)};

        int i = 0;
        foreach (Vector3Int cellPosition in cellPositions)
        {
            List<Cell> cellList = cell.gridRef.GetTileCells(worldPositions[i]);
            if (cellList == null)
            {
                i++;
                continue;
            }
            int rand = UnityEngine.Random.Range(0, 2)*16* extraTiles;
            int x = cellList[0].cellTerrain.binaryInList * 8 + cellList[1].cellTerrain.binaryInList *4 + cellList[2].cellTerrain.binaryInList *2 + cellList[3].cellTerrain.binaryInList + rand;
           


            TileBase tile = sortedTileBaseList[x];
            if (tile != null)
            {
                tilemap.SetTile(cellPosition, tile);
            }
            


            i++;
        }
    }

    private IEnumerator UpdateTileFromNoise(Cell cell, Tilemap tilemap, List<TileBase> sortedTileBaseList, int extraTiles = 0)
    {
        yield return null;

        Vector3 worldPosition = cell.worldPosition;
        float cellSize = cell.size;

        Vector3 pos1 = new Vector3(worldPosition.x - cellSize / 2, worldPosition.y + cellSize / 2, 0f);
        Vector3 pos2 = new Vector3(worldPosition.x + cellSize / 2, worldPosition.y + cellSize / 2, 0f);
        Vector3 pos3 = new Vector3(worldPosition.x - cellSize / 2, worldPosition.y - cellSize / 2, 0f);
        Vector3 pos4 = new Vector3(worldPosition.x + cellSize / 2, worldPosition.y - cellSize / 2, 0f);

        Vector3[] worldPositions = { pos1, pos2, pos3, pos4 };
        Vector3Int[] cellPositions = { tilemap.WorldToCell(pos1), tilemap.WorldToCell(pos2), tilemap.WorldToCell(pos3), tilemap.WorldToCell(pos4) };

        int i = 0;
        foreach (Vector3Int cellPosition in cellPositions)
        {
            List<Cell> cellList = cell.gridRef.GetTileCells(worldPositions[i]);
            if (cellList == null)
            {
                i++;
                continue;
            }   
            float freq= 0.255f;
            float noise1 = Mathf.PerlinNoise(cellList[0].worldPosition.x* freq, cellList[0].worldPosition.y* freq);
            float noise2 = Mathf.PerlinNoise(cellList[1].worldPosition.x * freq, cellList[1].worldPosition.y* freq);
            float noise3 = Mathf.PerlinNoise(cellList[2].worldPosition.x * freq, cellList[2].worldPosition.y * freq);
            float noise4 = Mathf.PerlinNoise(cellList[3].worldPosition.x * freq, cellList[3].worldPosition.y * freq);
            int cellbin1 =    cellList[0].cellTerrain.binaryInList;
            int cellbin2 =    cellList[1].cellTerrain.binaryInList;
            int cellbin3 =   cellList[2].cellTerrain.binaryInList;
            int cellbin4 =    cellList[3].cellTerrain.binaryInList;

            int in1 = Math.Max(cellbin1, noise1) > 0.5f ? 8 : 0;
            int in2 = Math.Max(cellbin2, noise2) > 0.5 ? 4 : 0;
            int in3 = Math.Max(cellbin3, noise3) > 0.5f ? 2 : 0;
            int in4 = Math.Max(cellbin4, noise4) > 0.5f ? 1 : 0;

            int x = in1 + in2 + in3 + in4;



            TileBase tile = sortedTileBaseList[x];
            if (tile != null)
            {
                tilemap.SetTile(cellPosition, tile);
            }



            i++;
        }
    }




}
