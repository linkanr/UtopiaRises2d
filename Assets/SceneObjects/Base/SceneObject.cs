using Pathfinding;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// All objects in the scene are based on this
/// </summary>
[RequireComponent(typeof(Collider2D))]

public abstract class SceneObject : MonoBehaviour, IPointerClickHandler, IClickableObject
{
    private readonly Dictionary<PickupTypes, StatsModifier> activeModifiers = new Dictionary<PickupTypes, StatsModifier>();
    [HideInInspector]
    public Collider2D c2D;
    [HideInInspector]
    public Rigidbody2D rB2D;
    protected Stats stats;
    public Sprite sprite;
    protected Bounds bounds;
    protected SoSceneObjectBase soBase;
    public bool isDead = false;
    private EffectSpriteOrganizer effectSpriteOrganizer;
    public SpriteRenderer spriteRenderer;

    /// <summary>
    /// sprite sorter are 
    /// </summary>
    protected SpriteSorter spriteSorter; // these are handled by stati




    
    
    
    protected virtual void Awake()
    {
        spriteSorter = new SpriteSorter(this.transform, spriteRenderer);
        spriteSorter.SortSprite();
        MouseOverScenObject mouseOverScenObject = gameObject.AddComponent<MouseOverScenObject>();
        mouseOverScenObject.Init(spriteRenderer);
        if (this is IDamageAble)
        {
            IDamageAble damageAble = this as IDamageAble;
            
            damageAble.idamageableComponent = gameObject.AddComponent<HealthHandler>();
            damageAble.idamageableComponent.Init(this);
        }
        GameObject effectSpriteGameObject = Resources.Load("EffectSpriteOrganizer") as GameObject;
        GameObject instance = Instantiate(effectSpriteGameObject,transform);
        effectSpriteOrganizer = instance.GetComponent<EffectSpriteOrganizer>() ;
        effectSpriteOrganizer.Init(this.transform);
       
    }

    protected virtual void Start()
    {
        
        c2D = GetComponent<Collider2D>();
        rB2D  = gameObject.AddComponent<Rigidbody2D>();
        OnCreated();
        rB2D.gravityScale = 0;
        bounds = c2D.bounds;
        AstarPath.active.UpdateGraphs(bounds);
    }
    public bool IsPickupActive(PickupTypes pickupType)
    {
        return activeModifiers.ContainsKey(pickupType);
    }

    public void AddPickup(PickupTypes pickupType, StatsModifier modifier)
    {
        activeModifiers[pickupType] = modifier;
        effectSpriteOrganizer.AddSpriteEffect(pickupType);
    }

    public void RemovePickup(PickupTypes pickupType)
    {
        effectSpriteOrganizer.RemoveSpriteEffect(pickupType);
        activeModifiers.Remove(pickupType);
    }

    public StatsModifier GetActiveModifier(PickupTypes pickupType)
    {
        return activeModifiers.TryGetValue(pickupType, out var modifier) ? modifier : null;
    }

    public void SetStats(Stats _stats)
    {
        stats = _stats;
        _stats.Add(StatsInfoTypeEnum.sceneObjectsTransform, this.transform  );

    }
    public Stats GetStats()
    {
        return stats;   
    }


    /// <summary>
    /// This adds the class to the list of Sceneobjects In the SceneObjectManager this should call public static Action<IDamageable> OnDamagableCreated
    /// </summary>
    public virtual void OnCreated()
    {
        BattleSceneActions.OnSceneObejctCreated(this);  
    }
    public void DestroySceneObject()//Calls whenever a sceneobject is destroyed
    {
        bounds = c2D.bounds;
        bounds.Expand(.1f);
        BattleSceneActions.OnUpdateBounds?.Invoke(bounds);
        BattleSceneActions.OnSceneObjectDestroyed(this);

        OnObjectDestroyed(); 
        
        
    }
    /// <summary>
    /// This is the specific implementation for each object
    /// </summary>
    protected abstract void OnObjectDestroyed(); // the specific implementation of the destruction

    /// <summary>
    /// On click effect with stats
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!MouseDisplayManager.instance.highligtSceneObjects)//check that we are not in mode for selecting it
        {
            Stats clickStats = new Stats();
            clickStats.AddStats(stats);
            AddStatsForClick(clickStats);

            OnClickObject.Create(clickStats);
        }
        

    }

    /// <summary>
    /// This Add extra stats that just shows up in ut information when obejct is clicked
    /// </summary>
    /// <param name="_stats"></param>
    protected abstract void AddStatsForClick(Stats _stats);

    public ClickableType GetClickableType()
    {
        return  ClickableType.SceneObject;
    }
}