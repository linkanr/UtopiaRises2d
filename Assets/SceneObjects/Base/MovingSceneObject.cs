using System;

using UnityEngine;

public abstract class MovingSceneObject : SceneObject
{
    
    private Cell currentCell;
    protected virtual void Update() 
    {
        Cell newCell =  GridCellManager.instance.gridConstrution.GetCellByWorldPosition(transform.position);
        if (newCell == currentCell)
        {
            return;
        }
        if (currentCell == null)
        {
            currentCell = newCell;
            currentCell.AddSceneObjects(this);
        }
        if (currentCell != newCell)
        {
            currentCell.containingSceneObjects.Remove(this);
            newCell.AddSceneObjects(this);  
            currentCell = newCell;
        }


        sceneObjectPosition = transform.position;
    }
    protected override void AddSpriteSorter(SpriteRenderer spriteRenderer)
    {
        SpriteSorter spriteSorter = gameObject.AddComponent<SpriteSorter>();
        spriteSorter.Init(spriteRenderer, true);
    }

}