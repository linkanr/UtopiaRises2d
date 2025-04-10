
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public class GridConstrution
{
    [SerializeField] public int sizeX { get; private set; }
    [SerializeField] public int sizeY { get; private set; }
    [SerializeField] public float cellSize { get; private set; }
    [SerializeField] private Vector3 offset;
    private static int idCounter = 0;
    public int instanceID;

    public Cell[,] gridArray;
    
    
   

    public GridConstrution(Texture2D texture, float cellSize, Vector3 offset)
    {
        instanceID = ++idCounter;
        Debug.Log($"[GridConstrution] Constructor called. ID: {instanceID}");
        this.sizeX = texture.width;
        this.sizeY = texture.height;

        this.offset = offset;
        this.cellSize = cellSize;
        gridArray = new Cell[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Color pixelColor = texture.GetPixel(x, y);
                CellReturnInfoArgs cellReturn = CellTerrainToColor.GetTerrain(pixelColor);
                CellTerrainEnum cellTerrain = cellReturn.cellTerrainEnum;
                string cellContains = cellReturn.cEllContainsEnum;

                CellTerrain cellTerrainObject = GridCellManager.instance.GetTerrainFromEneum(cellTerrain);
                if (cellTerrainObject == null)
                {
                    Debug.LogError("cellTerrainObject is null");
                }
                gridArray[x, y] = new Cell(x, y, this, cellTerrainObject);
                gridArray[x, y].cellStartingObject = cellContains;


            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x + cellSize * .5f, y + cellSize * .5f, 0f) * cellSize + new Vector3(offset.x, offset.y, offset.z);
    }

    public Vector3 GetCellPositionByPosition(Vector3 _position)
    {
        
        if (GetCellByWorldPosition(_position) == null)
        {
            return Vector3.zero;
        }
        Vector3 worldPos = GetWorldPosition(GetCellByWorldPosition(_position).x, GetCellByWorldPosition(_position).z);

        return worldPos;
    }
    public Vector3 GetCurrentCellPostionByMouse()
    {
        return GetCellPositionByPosition(WorldSpaceUtils.GetMouseWorldPosition());
    }
    public Cell GetCurrecntCellByMouse()
    {
        //Debug.Log("GetCurrecntCellByMouse");
        if (GetCellByWorldPosition(WorldSpaceUtils.GetMouseWorldPosition()) == null)
        {
            //Debug.Log("found nothing");
            return null;
        }
        return GetCellByWorldPosition(WorldSpaceUtils.GetMouseWorldPosition());
    }
    public Cell GetCellByWorldPosition(Vector3 position)
    {
      
        Vector3 offsetPos = position - new Vector3(offset.x, offset.y, offset.z);
        //Debug.Log($"[GetCellByWorldPosition] Input Pos: {position}, Offset: {offset}, OffsetPos: {offsetPos}");

        float maxX = sizeX * cellSize;
        float maxY = sizeY * cellSize;

        if (offsetPos.x >= 0f && offsetPos.y >= 0f && offsetPos.x <= maxX && offsetPos.y <= maxY)
        {
            int x = Mathf.FloorToInt(offsetPos.x / cellSize);
            int y = Mathf.FloorToInt(offsetPos.y / cellSize);

          //  Debug.Log($"[GetCellByWorldPosition] Within Bounds. Calculated Indices -> x: {x}, y: {y}");

            if (x >= 0 && y >= 0 && x < sizeX && y < sizeY)
            {
                var cell = gridArray[x, y];
                if (cell == null)
                {
                 //  Debug.LogWarning($"[GetCellByWorldPosition] Cell at x:{x}, y:{y} is NULL in gridArray.");
                }
                return cell;
            }
            else
            {
                Debug.LogError($"[GetCellByWorldPosition] Index out of gridArray range -> x: {x}, y: {y}, Grid Size: {sizeX}x{sizeY}");
                return null;
            }
        }
        else
        {
            //Debug.Log("cell size is " + cellSize + "size X is " + sizeX+ "size z is " + sizeY);
            //Debug.LogWarning($"[GetCellByWorldPosition] Position {position} is OUT OF BOUNDS after offset. Max Bounds: {maxX}, {maxY}");
            return null;
        }
    }

    public List<Cell> GetCellListByWorldPosition(Vector3 position, int size)
    {
        List<Cell> returnList = new List<Cell>();
        Cell startCell = GetCellByWorldPosition(position);
        int starty = startCell.z;
        int startx = startCell.x;
        for (int x = 0; x <= size * 2; x++)
        {
            int offsetX = x - size;
            int yOffsets = size - Mathf.Abs(offsetX);
            int yItterations = yOffsets * 2;

            for (int y = 0; y <= yItterations; y++)
            {
                int offsetY = y - yOffsets;
                returnList.Add(gridArray[offsetX + startx, offsetY + starty]);
            }
        }
        return returnList;

    }

    public List<Cell> GetCellListByWorldPosition(Vector3 position, int xSize, int ySize)
    {
        //Debug.Log("GetCellListByWorldPosition" + position);

 

        List<Cell> returnList = new List<Cell>();
        Cell startCell = GetCellByWorldPosition(position);
        if (startCell == null)
        {
            Debug.LogWarning("no start cell");
            if (gridArray == null)
            {
                Debug.LogError("gridArray is null");
            }
            return null;
        }
        int starty = startCell.z;
        int startx = startCell.x;

        // Loop for x dimension
        for (int x = -xSize / 2; x <= xSize / 2; x++)
        {
            int offsetX = x;
            int targetX = offsetX + startx;

            // Check x bounds
            if (targetX < 0 || targetX >= gridArray.GetLength(0))
                continue;

            // Loop for y dimension
            for (int y = -ySize / 2; y <= ySize / 2; y++)
            {
                int offsetY = y;
                int targetY = offsetY + starty;

                // Check y bounds
                if (targetY < 0 || targetY >= gridArray.GetLength(1))
                    continue;

                // Add the cell to the list
                returnList.Add(gridArray[targetX, targetY]);
            }
        }

        return returnList;
    }

    public List<Cell> GetTileCells(Vector3 postion)
    {
        List<Cell> returnList = new List<Cell>();
        Vector3 offset1 = new Vector3(-cellSize / 2, cellSize / 2, 0f);
        Vector3 offset2 = new Vector3(cellSize / 2, cellSize / 2, 0f);
        Vector3 offset3 = new Vector3(-cellSize / 2, -cellSize / 2, 0f);
        Vector3 offset4 = new Vector3(cellSize / 2, -cellSize / 2, 0f);
        Cell cell1 = GetCellByWorldPosition(postion + offset1);
        Cell cell2 = GetCellByWorldPosition(postion + offset2);
        Cell cell3 = GetCellByWorldPosition(postion + offset3);
        Cell cell4 = GetCellByWorldPosition(postion + offset4);
        returnList.Add(cell1);
        returnList.Add(cell2);
        returnList.Add(cell3);
        returnList.Add(cell4);
        if (cell1 == null || cell2 == null || cell3 == null || cell4 == null)
        {
            return null;
        }
        else
        {
            return returnList;
        }


    }


    public List<Cell> GetCellList()
    {
      
        List<Cell> returnList = new List<Cell>();
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                returnList.Add(gridArray[x, y]);
            }
        }
        return returnList;
    }
    public List<Cell> GetCellList(int step)
    {
        
        List<Cell> returnList = new List<Cell>();
        for (int x = 0; x < gridArray.GetLength(0); x+= step)
        {
            for (int y = 0; y < gridArray.GetLength(1); y+= step)
            {
                returnList.Add(gridArray[x, y]);
            }
        }
        return returnList;
    }

    internal Cell GetRandomNeighbour(Cell parent)
    {
        List<Cell> neighbors = new List<Cell>();

        int parentX = parent.x;
        int parentY = parent.z;

        // Check all 8 possible neighbors
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; // Skip the parent cell itself

                int checkX = parentX + x;
                int checkY = parentY + y;

                // Check if the neighbor is within bounds
                if (checkX >= 0 && checkX < sizeX && checkY >= 0 && checkY < sizeY)
                {
                    neighbors.Add(gridArray[checkX, checkY]);
                }
            }
        }

        if (neighbors.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, neighbors.Count);
            return neighbors[randomIndex];
        }

        return null;
    }
    
}
