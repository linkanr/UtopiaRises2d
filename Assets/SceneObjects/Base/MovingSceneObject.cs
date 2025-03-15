using System;

using UnityEngine;

public abstract class MovingSceneObject : SceneObject
{
    

    protected virtual void Update() 
    {
        if (WorldSpaceUtils.CrossedBorder(transform.position, sceneObjectPosition))
        {
            
            Cell oldCell = GridCellManager.Instance.gridConstrution.GetCellByWorldPosition(sceneObjectPosition);
            Cell newcell = GridCellManager.Instance.gridConstrution.GetCellByWorldPosition(transform.position);
            oldCell.containingSceneObjects.Remove(this);
            newcell.containingSceneObjects.Add(this);
        }

        
        sceneObjectPosition = transform.position;
        spriteSorter.SortSprite();
    }

}