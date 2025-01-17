using System;

using UnityEngine;

public abstract class MovingSceneObject : SceneObject
{
    protected virtual void OnEnable()
    {
        TimeActions.GlobalTimeChanged += UpdateBounds;
    }
    protected virtual void OnDisable()
    {
        TimeActions.GlobalTimeChanged -= UpdateBounds;
    }

    private void UpdateBounds(BattleSceneTimeArgs args)
    {
        bounds = c2D.bounds;
        bounds.Expand(.2f);
        AstarPath.active.UpdateGraphs(bounds);
    }
    protected virtual void Update() 
    {
        spriteSorter.SortSprite();
    }

}