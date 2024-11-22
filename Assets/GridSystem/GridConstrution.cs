
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        return new Vector3(x,y, 0f) * cellSize + new Vector3(offset.x,offset.y,offset.z);
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
            Debug.Log("found nothing");
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
