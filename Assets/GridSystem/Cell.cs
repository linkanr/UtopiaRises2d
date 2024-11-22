using System;
using System.Collections.Generic;
using UnityEngine;
public class Cell
{
    
    public Cell (int x, int y, GridConstrution grid)
    {
        this.x = x;
        this.y = y;
        gridRef = grid;
        size = grid.cellSize;

        
    }
    public int x;
    public int y;
    public float height;
    public GridConstrution gridRef;
    public string information;
    public float size;



}