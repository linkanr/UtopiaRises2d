using System;
using UnityEngine;

public static class BattleSceneActions
{
    
  
    public static Action OnInitializeScene;
 

    public static Action OnAllEnemiesSpawned;
    public static Action OnEmemyDefeated;
    /// <summary>
    /// Called when a scene object takes damage.
    /// </summary>
    public static Action<SceneObject> OnSceneObjectTakesPhysicalDamage;


    public static Action<Card> OnNewCardAdded;
    /// <summary>
    /// Action for a card draw, forces the card to be drawn.
    /// </summary>
    public static Action<int> OnDrawCard;
    /// <summary>
    /// Forces the ui to be rebudild
    /// </summary>
    public static Action OnCardsBeginAnimation;
    /// <summary>
    /// Stops the ui rebuild
    /// </summary>
    public static Action OnCardsEndAnimation;
    /// <summary>
    /// Called by the clock on a fixed interval.
    /// </summary>
    public static Action OnSpawnInterwallDone;
    /// <summary>
        
    /// Gets called when the spawing state is entered
    /// </summary>
    public static Action OnSpawningStarting;


    public static Action <Cell> OnCellClicked;



    public static Action<SceneObject> OnSceneObejctCreated;
    public static Action<int> OnBaseDamaged;
    public static Action<SceneObject> OnSceneObjectDestroyed;
    public static Action<Bounds> OnUpdateBounds;
    public static Action<int> OnFollowerCountChanged;
    public static Action<int> setInfluence; // this is used to directly set the influence
    public static Action <int> OnInfluenceChanged;
}

