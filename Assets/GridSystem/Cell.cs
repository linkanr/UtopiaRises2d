using System;
using System.Collections.Generic;
using UnityEngine;
public class Cell
{
    
    public Cell (int x, int y, GridConstrution grid, CellTerrain _cellTerrain, ObjectTypeEnums _containingSceneObject = ObjectTypeEnums.none )
    {
        this.x = x;
        this.y = y;
        gridRef = grid;
        size = grid.cellSize;
        cellTerrain = _cellTerrain;
        if (_containingSceneObject != ObjectTypeEnums.none)
        {
            EnviromentObject.Create(_containingSceneObject, new Vector3(x, y, 0f));
        }
 


    }
    
    public int x;
    public int y;
    public float height;
    public GridConstrution gridRef;
    public string information;
    public float size;
    public CellTerrain cellTerrain; 
    public CellEffect cellEffect;
    public SceneObject containingSceneObejct { get { return SceneObjectManager.Instance.sceneObjectGetter.GetSceneObject(worldPosition, maxDistance: .5f); }  }
    public Vector3 worldPosition { get { return gridRef.GetWorldPostion(x, y); } }
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