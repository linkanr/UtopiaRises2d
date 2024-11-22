using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public static class BattleSceneActions
{
    
    public static Action OnPreSceneInit;
    public static Action OnPreSceneDone;
    public static Action OnInitializeScene;

    public static Action<Card> OnNewCardAdded;
    public static Action<int> OnDrawCard;
    public static Action OnCardsBeginDrawn;
    public static Action <Cell> OnCellClicked;
    public static Action OnAllEnemiesSpawned;
    public static Action<bool> OnCardLocked;
    public static Action OnCardsEndDrawn;
    public static Action<ITargetableByEnemy> OnTargetableCreated;
    public static Action<ITargetableByEnemy> OnTargetableDestroyed;
    public static Action<Bounds> OnUpdateBounds; 

    public static Action<Transform> OnFollowerCreated;
    public static Action<Transform> OnFollowerDestroyed;
    public static Action<int> OnFollowerCountChanged;

    public static Action<BattleSceneTimeArgs> GlobalTimeChanged;
    public static Action OnSecondChange;
    public static Action<bool> OnPause;

    public static Action<int> setInfluence; // this is used to directly set the influence
    public static Action <int> OnInfluenceChanged;
}

public class BattleSceneTimeArgs
{
    public float time;
    public float deltaTime;
}