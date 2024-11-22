using Pathfinding;
using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// All objects in the scene are based on this
/// </summary>
[RequireComponent(typeof(Collider2D))]

public abstract class SceneObject:MonoBehaviour, IPointerClickHandler
{

    public Collider2D c2D;
    protected Stats stats;
    protected Bounds bounds;
    protected SoSceneObjectBase soBase;
    
   
    

    protected virtual void Start()
    {
        c2D = GetComponent<Collider2D>();
        bounds = c2D.bounds;
        var guo = new GraphUpdateObject(bounds);
        AstarPath.active.UpdateGraphs(bounds);

       


    }

    public void SetStats(Stats _stats)
    {
        stats = _stats;
        _stats.Add(StatsInfoTypeEnum.objectToFollow, this.transform  );

    }




    protected void DestroySceneObject()//Calls whenever a sceneobject is destroyed
    {
        bounds = c2D.bounds;
        BattleSceneActions.OnUpdateBounds?.Invoke(bounds);
        OnObjectDestroyed(); 
        
        
    }

    protected abstract void OnObjectDestroyed(); // the specific implementation of the destruction


    public void OnPointerClick(PointerEventData eventData)
    {
 

        OnClickObject.Create(stats);
    }

    
  



}