using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public static class BattleSceneActions
{
    
  
    public static Action OnInitializeScene;
 
    public static Action OnAllEnemiesDestroyed;
    public static Action OnEnemyBaseDestroyed;


    public static Action<Card> OnNewCardAdded;
    public static Action<int> OnDrawCard;
    public static Action OnCardsBeginDrawn;
    public static Action OnCardsEndDrawn;
    public static Action OnSpawnInterwallDone;
    public static Action OnLiveStatsStarting;


    public static Action <Cell> OnCellClicked;



    public static Action<SceneObject> OnSceneObejctCreated;
    public static Action<int> OnBaseDamaged;
    public static Action<SceneObject> OnSceneObjectDestroyed;
    public static Action<Bounds> OnUpdateBounds;
    public static Action<int> OnFollowerCountChanged;
    public static Action<int> setInfluence; // this is used to directly set the influence
    public static Action <int> OnInfluenceChanged;
}

