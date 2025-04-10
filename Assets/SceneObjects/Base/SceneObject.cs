using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System.Collections;
using System;
using Pathfinding;
using Pathfinding.RVO;

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

    public SceneObjectAnimator objectAnimator;

    /// <summary>
    /// The effect sprite organizer for managing sprite effects.
    /// </summary>
    private EffectSpriteOrganizer effectSpriteOrganizer;

    /// <summary>
    /// The sprite renderer for the scene object.
    /// </summary>
    public SpriteRenderer spriteRenderer;



    public HealthSystem healthSystem;

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
        //spriteSorter = new SpriteSorter(this.transform, spriteRenderer);
        //spriteSorter.SortSprite();
        MouseOverScenObject mouseOverScenObject = gameObject.AddComponent<MouseOverScenObject>();
        mouseOverScenObject.Init(spriteRenderer);
        GameObject effectSpriteGameObject = Resources.Load("EffectSpriteOrganizer") as GameObject;
        GameObject instance = Instantiate(effectSpriteGameObject, transform);
        effectSpriteOrganizer = instance.GetComponent<EffectSpriteOrganizer>();
        effectSpriteOrganizer.Init(this.transform);

        statsHandler = new SceneObjectStatsHandler(this, effectSpriteOrganizer);



    }



    /// <summary>
    /// This initializes the scene object and adds health system if needed
    /// </summary>
    public virtual void InitilizeFromSo()
    {
        sceneObjectPosition = transform.position;
        BattleSceneActions.OnSceneObjectCreated(this);
        
        
        OnCreated();

        spriteRenderer.sprite = GetStats().sprite;
        AddSpriteSorter(spriteRenderer);


        if (GetStats().health > 0)
        {
            healthSystem =gameObject.AddComponent<PhysicalHealthSystem>();
            healthSystem.Init(GetStats().health,this);

        }
        if (GetStats().lifeTime > 0)
        {
            healthSystem = gameObject.AddComponent<TimeHealthSystem>();
            healthSystem.Init( GetStats().lifeTime, this);
            GameObject towerUiGO = Instantiate(Resources.Load("ui"), transform) as GameObject;
            TowerTimeUiHandle towerUi = towerUiGO.GetComponent<TowerTimeUiHandle>(); // Should only be added if it has a damager
            towerUi.Init(this);
        }
        Cell cell = GridCellManager.instance.gridConstrution.GetCellByWorldPosition(transform.position);

        if (cell == null)
        {
            Debug.Log("cell not found");

        }
        cell.AddSceneObjects(this);
    }

    protected virtual void AddSpriteSorter(SpriteRenderer spriteRenderer)
    {
        SpriteSorter spriteSorter = gameObject.AddComponent<SpriteSorter>();
        spriteSorter.Init(spriteRenderer, false);
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
    /// Called when the scene object is created. This is the specific implementation of the child. Called from intialized from SO on the base class
    /// </summary>
    public abstract void OnCreated();



    public void KillSceneObject() 
    {
        if (healthSystem != null)
        {

                healthSystem.Die(null);
           
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
        if (isDead) return; // Prevent multiple calls to destruction
        isDead = true; // Mark as dead to prevent multiple calls
        Debug.Log("Scene object destroyed implementation");
        BattleSceneActions.OnSceneObjectDestroyed(this);

        // Remove from grid
        Cell cell = GridCellManager.instance.gridConstrution.GetCellByWorldPosition(transform.position);
        if (cell != null && cell.containingSceneObjects.Contains(this))
        {
            cell.containingSceneObjects.Remove(this);
        }

        // Play visual effects
        VisualEffectManager.PlayVisualEffect(GetDeathEffect(), transform.position);

        // Trigger any custom object cleanup
        OnObjectDestroyedObjectImplementation();

        // Listen for animation complete
        if (objectAnimator != null)
        {
            objectAnimator.OnDeathAnimationFinished = () =>
            {
                Destroy(gameObject); // Actual destruction happens here
            };

            objectAnimator.PlayDeath(); // Start death animation
            GraphicalCleanup();
        }
        else
        {
            Destroy(gameObject); // If no animator, destroy immediately
        }

    }

    private void GraphicalCleanup()
    {
        spriteRenderer.sortingLayerName = "EnviromentObjects"; // Reset sorting layer
        AIPath aIPath = GetComponent<AIPath>();
        Collider2D collider2D = GetComponent<Collider2D>();
        RVOController rVOController = GetComponent<RVOController>();
        AIPathVisualizer aIPathVisualizer = GetComponent<AIPathVisualizer>();
        if (aIPathVisualizer != null)
        {
            Destroy(aIPathVisualizer); // Destroy AI path visualizer
        }
        if (rB2D != null)
        {
            Destroy(rB2D); // Destroy Rigidbody2D
        }
        if (rVOController != null)
        {
            rVOController.enabled = false; // Disable RVO controller
            Destroy(rVOController); // Destroy RVO controller
        }


        if (aIPath != null)
        {
            aIPath.canMove = false; // Stop movement
            Destroy(aIPath); // Destroy AI path
        }
        if (collider2D != null)
        {
            // Destroy collider
            bounds = collider2D.bounds;
            bounds.Expand(.2f);
            Destroy(collider2D);
            StartCoroutine(DelayedTagUpdateUntilReady(bounds));

        }
    }
    private IEnumerator DelayedTagUpdateUntilReady(Bounds bounds)
    {
        Vector3 testPos = bounds.center;
        int maxTries = 10;
        int attempts = 0;
        bool foundValidCollider = false;

        while (attempts < maxTries)
        {
            yield return null;
            Physics2D.SyncTransforms();

            var hits = Physics2D.OverlapBoxAll(testPos, new Vector2(0.3f, 0.3f), 0f, TagFromLayerZ.instance.layerMask);

            foreach (var hit in hits)
            {
                string layerName = LayerMask.LayerToName(hit.gameObject.layer).ToLower();
                if (layerName.Contains("water"))
                {
                    foundValidCollider = true;
                    break;
                }
            }

            if (foundValidCollider) break;
            attempts++;
        }

        if (foundValidCollider)
        {
            TagFromLayerZ.instance.AssignTags(bounds);
        }
        else
        {
            Debug.LogWarning("⚠️ Could not detect water collider after explosion. Tags not updated.");
        }
    }


    /// <summary>
    /// Called when the scene object is destroyed.Only needs to implement specific logic for the scene object.The call to the list is allready done
    /// </summary>
    /// 
    protected virtual visualEffectsEnum GetDeathEffect()
    {
        return visualEffectsEnum.minor;
    }
    protected abstract void OnObjectDestroyedObjectImplementation();

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
