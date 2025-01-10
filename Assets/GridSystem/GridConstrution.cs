
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public class GridConstrution
{
    [SerializeField]public int sizeX { get; private set; }
    [SerializeField] public int sizeY { get; private set; }
    [SerializeField] public  float cellSize { get; private set; }
    [SerializeField] private Vector3 offset;
    
    public Cell [,] gridArray;
    public GridConstrution (int sizeX, int sizeY, float cellSize, Vector3 offset)
    {
       
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.cellSize = cellSize;
        this.offset = offset;
        gridArray = new Cell[sizeX, sizeY];
        for (int x = 0; x<gridArray.GetLength(0); x++)
        {
            for (int y = 0; y<gridArray.GetLength(1); y++)
            {
                Cell newCell = new Cell(x, y, this);
                gridArray[x, y] = newCell;
                //newCell.height = GetCellHeight(newCell);

            }
        }

    }

    private float GetCellHeight(Cell newCell)
    {
        Vector3 worldPos = GetWorldPostion(newCell.x, newCell.y);
        worldPos.y = +10f;
        Ray ray = new Ray(worldPos,new Vector3(0f,-1f,0f));
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray ,out RaycastHit hitData,1000f, GameSceneRef.instance.collisionLayerGrid))
    
        {
            Debug.Log("setting cell y to " + hitData.point.y);
           return hitData.point.y;
            
        }
        
        else {  Debug.Log("no depth info for cell"); return -1; }  
        
    }

    public Vector3 GetWorldPostion(int x, int y)
    {
        return new Vector3(x+cellSize*.5f,y+cellSize*.5f, 0f) * cellSize + new Vector3(offset.x,offset.y,offset.z);
    }

    public Vector3 GetCellPositionByPosition(Vector3 _position)
    {
        if (GetCellByWorldPostion(_position) == null)
        {
            return Vector3.zero;
        }
        Vector3 worldPos =  GetWorldPostion(GetCellByWorldPostion(_position).x,GetCellByWorldPostion(_position).y);
        worldPos.x += cellSize / 2;
        worldPos.y += cellSize / 2;
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
        //Debug.Log(offsetPos);
        if (offsetPos.x>0f && offsetPos.y>0f && offsetPos.x <= (float) sizeX* cellSize && offsetPos.y<= (float) sizeY * cellSize)
        {
            int x = Mathf.FloorToInt(offsetPos.x / cellSize);
            int y = Mathf.FloorToInt(offsetPos.y / cellSize);
            //Debug.Log(x + "  " + y);
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
        for (int x =0; x<= size*2; x++)
        {
            int offsetX = x-size;
            int yOffsets = size - Mathf.Abs(offsetX);
            int yItterations = yOffsets * 2;

            for (int y = 0; y<= yItterations; y++)
            {
                int offsetY = y - yOffsets;
                returnList.Add(gridArray[offsetX+startx, offsetY+starty]);
            }
        }
        return returnList;

    }

    public List<Cell> GetCellListByWorldPosition(Vector3 position, int xSize, int ySize)
    {
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




    public void SetTextToCellList(List<Cell> cells)
    {
        foreach(Cell cell in cells)
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


}
