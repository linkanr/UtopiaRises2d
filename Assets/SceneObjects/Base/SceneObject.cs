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

public abstract class SceneObject : MonoBehaviour, IPointerClickHandler
{

    public Collider2D c2D;
    public Rigidbody2D rB2D;
    protected Stats stats;

    protected Bounds bounds;
    protected SoSceneObjectBase soBase;
    




    protected virtual void Start()
    {
        c2D = GetComponent<Collider2D>();
        rB2D  = gameObject.AddComponent<Rigidbody2D>();
        rB2D.gravityScale = 0;

        bounds = c2D.bounds;
        var guo = new GraphUpdateObject(bounds);
        AstarPath.active.UpdateGraphs(bounds);

       


    }

    public void SetStats(Stats _stats)
    {
        stats = _stats;
        _stats.Add(StatsInfoTypeEnum.objectToFollow, this.transform  );

    }
    public Stats GetStats()
    {
        return stats;   
    }



    protected void DestroySceneObject()//Calls whenever a sceneobject is destroyed
    {
        bounds = c2D.bounds;
        BattleSceneActions.OnUpdateBounds?.Invoke(bounds);
        if (GetType() == typeof(IDamageable))
        {
            BattleSceneActions.OnDamagableDestroyed(this as IDamageable);
        }
        OnObjectDestroyed(); 
        
        
    }

    protected abstract void OnObjectDestroyed(); // the specific implementation of the destruction


    public void OnPointerClick(PointerEventData eventData)
    {

        Stats clickStats = new Stats();
        clickStats.AddStats(stats);
        AddStatsForClick(clickStats);
        
        OnClickObject.Create(clickStats);
    }

    /// <summary>
    /// This Add extra stats that just shows up in ut information when obejct is clicked
    /// </summary>
    /// <param name="_stats"></param>
    protected abstract void AddStatsForClick(Stats _stats);



}