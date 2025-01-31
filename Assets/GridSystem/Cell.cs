using System;
using System.Collections.Generic;
using UnityEngine;
public class Cell
{
    
    public Cell (int x, int y, GridConstrution grid, CellTerrain _cellTerrain )
    {
        this.x = x;
        this.y = y;
        gridRef = grid;
        size = grid.cellSize;
        cellTerrain = _cellTerrain;

 


    }
    
    public int x;
    public int y;
    public float height;
    public GridConstrution gridRef;
    public string information;
    public float size;
    public CellTerrain cellTerrain; 
    public CellEffect cellEffect;

    public SceneObject[] containingSceneObjects { get { return SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(worldPosition, maxDistance: .5f).ToArray(); }  }
    public SceneObject containingEnvObject { get { return SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(worldPosition, maxDistance: .5f).Find(x => x is EnviromentObject); } }
    public Vector3 worldPosition { get { return gridRef.GetWorldPostion(x, y); } }
    public void CreateCellEffect(CellEffectEnum _cellEffect)
    {
        if (cellEffect != null)
        {
            cellEffect.RemoveCellEffect(); // Ensure old effect is properly removed
        }

        cellEffect = CellEffectCreator.CreateCellEffect(_cellEffect, this);

        // Log to confirm effect creation
        Debug.Log($"Cell Effect Created: {_cellEffect} at {worldPosition}");

        // The action call triggers the update in the Tilemap
        CellActions.UpdateCellEffect.Invoke(new CellEffectUpdateArgs { cell = this, cellEffect = _cellEffect });
    }

    public void RemoveCellEffect()
    {
        
        CellActions.UpdateCellEffect.Invoke(new CellEffectUpdateArgs { cell = this,cellEffect = CellEffectEnum.None });
    }

    public bool hasSceneObejct
    {
        get
        {
            if (SceneObjectManager.Instance.sceneObjectGetter.GetSceneObject(worldPosition, maxDistance:.5f)== null)
                {

                    return false;
                }
                else
                {
                
                    return true;
                }
            }

    }
    public float GetWalkPenalty()
    {
        float walkPenalty =1f; 
        if (!hasSceneObejct)
        {
            walkPenalty = cellTerrain.walkPenalty;
        }
        else
        {
            walkPenalty = HandleSceneObjectWalkPenalty(walkPenalty);
        }
        return walkPenalty;

    }

    private float HandleSceneObjectWalkPenalty(float input)
    {
        float walkPenalty = input;
        foreach (SceneObject sceneObject in containingSceneObjects)
        {
            

            if (sceneObject is not EnviromentObject)
            {
                continue;
            }
        if (sceneObject is EnviromentObject)
            {

                walkPenalty = sceneObject.GetStats().moveFactor;
            }
            if (cellEffect != null)
            {
                walkPenalty *= cellEffect.walkPenalty;
            }
            

           
        }
        return walkPenalty;
    }
}