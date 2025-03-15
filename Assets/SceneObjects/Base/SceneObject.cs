using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

/// <summary>
/// Represents a base class for all scene objects in the game.
/// </summary>
public abstract class SceneObject : MonoBehaviour, IPointerClickHandler, IClickableObject
{



    /// <summary>
    /// The Collider2D component attached to the scene object.
    /// </summary>
    [HideInInspector] public Collider2D c2D;

    /// <summary>
    /// The Rigidbody2D component attached to the scene object.
    /// </summary>
    [HideInInspector] public Rigidbody2D rB2D;

    public Vector3 sceneObjectPosition;
    /// <summary>
    /// The bounds of the scene object.
    /// </summary>
    protected Bounds bounds;

    /// <summary>
    /// The base data for the scene object.
    /// </summary>
    protected SoSceneObjectBase soBase;

    /// <summary>
    /// Indicates whether the scene object is dead.
    /// </summary>
    public bool isDead = false;

    /// <summary>
    /// The effect sprite organizer for managing sprite effects.
    /// </summary>
    private EffectSpriteOrganizer effectSpriteOrganizer;

    /// <summary>
    /// The sprite renderer for the scene object.
    /// </summary>
    public SpriteRenderer spriteRenderer;

    /// <summary>
    /// The sprite sorter for sorting the sprite.
    /// </summary>
    protected SpriteSorter spriteSorter;

    /// <summary>
    /// The stats handler for managing the scene object's stats.
    /// </summary>
    private SceneObjectStatsHandler statsHandler;

    /// <summary>
    /// Initializes the scene object by setting up various components and handlers.
    /// </summary>
    /// <remarks>
    /// This method performs the following actions:
    /// <list type="bullet">
    /// <item><description>Initializes the sprite sorter and sorts the sprite.</description></item>
    /// <item><description>Adds and initializes the MouseOverScenObject component.</description></item>
    /// <item><description>If the scene object implements IDamageAble, adds and initializes the HealthHandler component.</description></item>
    /// <item><description>Loads, instantiates, and initializes the EffectSpriteOrganizer component.</description></item>
    /// <item><description>Initializes the SceneObjectStatsHandler with the effect sprite organizer.</description></item>
    /// </list>
    /// </remarks>
    protected virtual void Awake()
    {
        spriteSorter = new SpriteSorter(this.transform, spriteRenderer);
        spriteSorter.SortSprite();
        MouseOverScenObject mouseOverScenObject = gameObject.AddComponent<MouseOverScenObject>();
        mouseOverScenObject.Init(spriteRenderer);
        GameObject effectSpriteGameObject = Resources.Load("EffectSpriteOrganizer") as GameObject;
        GameObject instance = Instantiate(effectSpriteGameObject, transform);
        effectSpriteOrganizer = instance.GetComponent<EffectSpriteOrganizer>();
        effectSpriteOrganizer.Init(this.transform);

        statsHandler = new SceneObjectStatsHandler(this, effectSpriteOrganizer);



    }

    /// <summary>
    /// Called when the scene object is created.
    /// </summary>
    protected virtual void Start()
    {
        //c2D = GetComponent<Collider2D>();
        //rB2D = gameObject.AddComponent<Rigidbody2D>();
        OnCreated();
        //rB2D.gravityScale = 0;
        //bounds = c2D.bounds;
        spriteRenderer.sprite = GetStats().sprite;
        if (this is IDamageAble)
        {
            IDamageAble damageAble = this as IDamageAble;
            if (this is IHasLifeSpan)
            {
                IHasLifeSpan hasLifeSpan = this as IHasLifeSpan;
                damageAble.iDamageableComponent = gameObject.AddComponent<TimeHealthHandler>();
                damageAble.iDamageableComponent.Init(this);
                GameObject towerUiGO = Instantiate(Resources.Load("ui"), transform) as GameObject;
                TowerTimeUiHandle towerUi = towerUiGO.GetComponent<TowerTimeUiHandle>();
                towerUi.Init(this);
            }
            else
            {

                damageAble.iDamageableComponent = gameObject.AddComponent<PhysicalHealthHandler>();
                damageAble.iDamageableComponent.Init(this);
            }


        }


    }

    /// <summary>
    /// Sets the stats for the scene object.
    /// </summary>
    /// <param name="newStats">The new stats to set.</param>
    public void SetStats(Stats newStats)
    {
        statsHandler.SetStats(newStats);
    }

    /// <summary>
    /// Gets the stats of the scene object.
    /// </summary>
    /// <returns>The current stats of the scene object.</returns>
    public Stats GetStats()
    {
        return statsHandler.GetStats();
    }

    /// <summary>
    /// Gets the stats handler for the scene object.
    /// </summary>
    /// <returns>The stats handler.</returns>
    public SceneObjectStatsHandler GetStatsHandler()
    {
        return statsHandler;
    }

    /// <summary>
    /// Called when the scene object is created.
    /// </summary>
    public virtual void OnCreated()
    {
        sceneObjectPosition = transform.position;
        BattleSceneActions.OnSceneObejctCreated(this);
        Cell cell = GridCellManager.Instance.gridConstrution.GetCellByWorldPosition(transform.position);

        if (cell == null)
        {
            Debug.Log("cell not found");

        }
        cell.AddSceneObjects(this);
    }


    public void KillSceneObject() 
    {
        if (this is IDamageAble)
        {
            IDamagableComponent idamagableComponent = (this as IDamageAble).iDamageableComponent;
            if (idamagableComponent != null)
            {
                idamagableComponent.Die();
            }
        }
        else
        {
            OnSceneObjectDestroyedBase();
        }
    }

    /// <summary>
    /// CAlled when the scene object is destroyed.
    /// </summary>
    public void OnSceneObjectDestroyedBase()
    {

        BattleSceneActions.OnSceneObjectDestroyed(this);
        Cell cell = GridCellManager.Instance.gridConstrution.GetCellByWorldPosition(transform.position);
        if (cell != null) 
        {
            if (cell.containingSceneObjects.Contains(this))
            {
                cell.containingSceneObjects.Remove(this);
            }
        }
        VisualEffectManager.PlayVisualEffect(GetDeathEffect(), transform.position);
        OnObjectDestroyedObjectImplementation();
    }

    /// <summary>
    /// Called when the scene object is destroyed.Only needs to implement specific logic for the scene object.The call to the list is allready done
    /// </summary>
    protected abstract void OnObjectDestroyedObjectImplementation();
    protected virtual visualEffectsEnum GetDeathEffect()
    {
        return visualEffectsEnum.death;
    }
    /// <summary>
    /// Handles pointer click events on the scene object.
    /// </summary>
    /// <param name="eventData">The event data for the pointer click.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!MouseDisplayManager.instance.highligtSceneObjects)
        {

            Stats clickStats = new Stats();
            clickStats = StatsCopier.CopyStats(statsHandler.GetStats());


            AddStatsForClick(clickStats);



            OnClickObject.Create(clickStats); // this does not take the modifiers into account. 
        }
    }

    /// <summary>
    /// Adds stats for a click event.
    /// </summary>
    /// <param name="_stats">The stats to add.</param>
    protected abstract void AddStatsForClick(Stats _stats);

    /// <summary>
    /// Gets the clickable type of the scene object.
    /// </summary>
    /// <returns>The clickable type.</returns>
    public ClickableType GetClickableType()
    {
        return ClickableType.SceneObject;
    }
}
