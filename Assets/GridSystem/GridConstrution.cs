
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


    public Cell[,] gridArray;
    
    
    public GridConstrution(int sizeX, int sizeY, float cellSize, Vector3 offset, List<CellTerrain> _cellTerrainList, float xmulti, float ymulti)
    {
        Debug.Log($"cellTerrainList: {(_cellTerrainList == null ? "null" : "not null")}");
        if (_cellTerrainList != null)
        {
            Debug.Log($"cellTerrainList count: {_cellTerrainList.Count}");
        }
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.cellSize = cellSize;
        this.offset = offset;
        gridArray = new Cell[sizeX, sizeY];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                float fx = (float)x*1.35f;
                float fy = (float)y*1.35f;
                float rand = Mathf.PerlinNoise(fx * xmulti, fy * ymulti);
                //Debug.Log($"x: {fx}, y: {fy}, PerlinNoise: {rand}");
                CellTerrain cellTerrain = null;
                ObjectTypeEnums objectType = ObjectTypeEnums.none;
                if (rand > 0.7f) /// genereate water
                {
                    cellTerrain = _cellTerrainList[1];
                }
                else
                {
                    cellTerrain = _cellTerrainList[0];
                    if (rand < .2f)
                    {
                        objectType = ObjectTypeEnums.forest;
                    }
                    if (rand < .1f)
                    {
                        objectType = ObjectTypeEnums.stone;
                    }

                }
                Cell newCell = new Cell(x, y, this, cellTerrain,objectType);
                gridArray[x, y] = newCell;
            }
        }

    }

    public GridConstrution(Texture2D texture, List<CellTerrain> _cellTerrainList, float cellSize, Vector3 offset)
    {
        this.sizeX = texture.width;
        this.sizeY = texture.height;
        Debug.Log("sizeX: " + sizeX + " sizeY: " + sizeY);
        this.cellSize = cellSize;
        this.offset = offset;
        gridArray = new Cell[sizeX, sizeY];
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                Color pixelColor = texture.GetPixel(x, y);

                CellTerrainEnum cellTerrain = CellTerrainToColor.GetTerrain(pixelColor);
                CellTerrain cellTerrainObject = _cellTerrainList.Find(x => x.cellTerrainEnum == cellTerrain);
                if (cellTerrainObject == null)
                {
                    Debug.LogError("cellTerrainObject is null");
                }
                gridArray[x, y] = new Cell(x, y, this, cellTerrainObject);
                //Debug.Log("cellTerrainObject: " + cellTerrainObject.cellTerrainEnum + " at " + x +" "+ y);
            }
        }
    }

    public Vector3 GetWorldPostion(int x, int y)
    {
        return new Vector3(x + cellSize * .5f, y + cellSize * .5f, 0f) * cellSize + new Vector3(offset.x, offset.y, offset.z);
    }

    public Vector3 GetCellPositionByPosition(Vector3 _position)
    {
        if (GetCellByWorldPostion(_position) == null)
        {
            return Vector3.zero;
        }
        Vector3 worldPos = GetWorldPostion(GetCellByWorldPostion(_position).x, GetCellByWorldPostion(_position).y);

        return worldPos;
    }
    public Vector3 GetCurrentCellPostionByMouse()
    {
        return GetCellPositionByPosition(WorldSpaceUtils.GetMouseWorldPosition());
    }
    public Cell GetCurrecntCellByMouse()
    {
        if (GetCellByWorldPostion(WorldSpaceUtils.GetMouseWorldPosition()) == null)
        {
            return null;
        }
        return GetCellByWorldPostion(WorldSpaceUtils.GetMouseWorldPosition());
    }
    public Cell GetCellByWorldPostion(Vector3 position)
    {

        Vector3 offsetPos = position - new Vector3(offset.x, offset.y, offset.z);
  
        if (offsetPos.x >= 0f && offsetPos.y >= 0f && offsetPos.x <= (float)sizeX * cellSize && offsetPos.y <= (float)sizeY * cellSize)
        {
            int x = Mathf.FloorToInt(offsetPos.x / cellSize);
            int y = Mathf.FloorToInt(offsetPos.y / cellSize);
            //Debug.Log("looking at " + x + "  " + y);
            return gridArray[x, y];
        }
        else
        {
            //Debug.Log("found nothing");
            return null;
        }
    }
    public List<Cell> GetCellListByWorldPosition(Vector3 position, int size)
    {
        List<Cell> returnList = new List<Cell>();
        Cell startCell = GetCellByWorldPostion(position);
        int starty = startCell.y;
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
        Cell startCell = GetCellByWorldPostion(position);
        int starty = startCell.y;
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
        Cell cell1 = GetCellByWorldPostion(postion + offset1);
        Cell cell2 = GetCellByWorldPostion(postion + offset2);
        Cell cell3 = GetCellByWorldPostion(postion + offset3);
        Cell cell4 = GetCellByWorldPostion(postion + offset4);
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


    public void SetTextToCellList(List<Cell> cells)
    {
        foreach (Cell cell in cells)
        {
            cell.information = "test";

        }
    }
    public void SetCellText(Vector3 position, string value)
    {
        Cell cell = GetCellByWorldPostion(position);
        if (cell != null)
        {
            GetCellByWorldPostion(position).information = value;


        }
        else
        {
            Debug.Log("no cell");
        }

    }
    public void SetCellText(Cell cell, string value)
    {

        cell.information = value;




    }

    public List<Cell> GetCellList()
    {
        Debug.Log("GetCellList");
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
        Debug.Log("GetCellList");
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
}
