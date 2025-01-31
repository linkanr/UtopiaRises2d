using System;

using UnityEngine;

public abstract class MovingSceneObject : SceneObject
{

    protected virtual void Update() 
    {
        spriteSorter.SortSprite();
    }

}