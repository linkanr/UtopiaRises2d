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
    public CellTerrain cellTerrain; 
    public CellEffect cellEffect;
    public SceneObject containingSceneObejct { get { return SceneObjectManager.Instance.sceneObjectGetter.GetSceneObject(worlPosition, maxDistance:.5f); } }
    public Vector3 worlPosition { get { return gridRef.GetWorldPostion(x, y); } }
    public bool hasSceneObejct
    {
        get
        {
            if (containingSceneObejct == null)
            {

                return false;
            }
            else
            {
                Debug.Log("scene objet" + containingSceneObejct.GetStats().GetString(StatsInfoTypeEnum.name));
                return true;
            }
        }

    }
}