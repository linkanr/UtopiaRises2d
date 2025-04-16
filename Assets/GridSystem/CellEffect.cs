using System;
using UnityEngine;



public class CellEffect : IDisposable
{
    public CellEffectEnum cellTerrainEnum;
    public float walkPenalty;
    public float damageMulti;
    public int damagePerSecond;
    public Sprite sprite;
    public Cell parent;
    private bool disposed = false; // To track whether Dispose has been called

    public CellEffect(Cell parentCell)
    {
        parent = parentCell;
        TimeActions.OnSecondChange += OnSecondUpdate; // Subscribe to event
    }

    internal void OnSecondUpdate()
    {
        if (disposed) return;
        if (parent.heat > 0.1f || parent.burning)
        {
            TakeDamage();
        }
        
    }

    private void TakeDamage()
    {
        
        SceneObject[] sceneObjects = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(parent.worldPosition, maxDistance: 1.1f).ToArray();
        //Debug.Log($"SceneObjects: {sceneObjects.Length}" + "at world position" + parent.worldPosition);
       
        int burnDamage = 0;
        if (parent.burning)
        {
            burnDamage = 1;
        }
        foreach (SceneObject sceneObject in sceneObjects)
        {
            if (sceneObject == null) 
            {
                Debug.LogWarning("SceneObject is null");
                return;
            }

            int firedamage = sceneObject.GetStats().takesDamageFrom.damageFromFire;

            if (sceneObject.healthSystem != null && firedamage > 0)
            {
                int totalDamage = (Mathf.FloorToInt(parent.heat * 10f) + burnDamage) * firedamage;
                if (totalDamage > 0)
                {
                    if (sceneObject?.transform != null)
                    {
                        sceneObject.healthSystem.TakeDamage(totalDamage, null);
                     //   Debug.Log("SceneObject is not null");
                    }
                    
                }
                
            }
        }
    }

    public void RemoveCellEffect()
    {
        Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Unsubscribe from events to avoid memory leaks
                TimeActions.OnSecondChange -= OnSecondUpdate;

                // Notify the parent to remove this effect
                parent?.RemoveCellEffect();

                // Notify listeners that the effect is gone
                CellActions.UpdateCellEffect?.Invoke(new CellEffectUpdateArgs
                {
                    cell = parent,
                    cellEffect = CellEffectEnum.None
                });
            }

            disposed = true;
        }
    }

    ~CellEffect()
    {
        Dispose(false);
    }
}



internal class CellEffectCreator
{
    internal static CellEffect CreateCellEffect(CellEffectEnum cellEffectEnum, Cell parent)
    {
        CellEffect cellEffect = new CellEffect(parent);


        switch (cellEffectEnum)
        {

            case CellEffectEnum.Gas:
                cellEffect.cellTerrainEnum = CellEffectEnum.Gas;
                cellEffect.walkPenalty = .5f;



                
                break;
            case CellEffectEnum.Fire:
                cellEffect.cellTerrainEnum = CellEffectEnum.Fire;
                cellEffect.walkPenalty = .5f;
  
                


                break;
        }
        TimeActions.OnSecondChange += cellEffect.OnSecondUpdate;
        return cellEffect;
    }


}
