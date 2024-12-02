using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectManager : MonoBehaviour

{
    public static SceneObjectManager Instance;
    public List<Transform> followerList;
    public int influence  {get; private set;}
    public List<IDamageable> iDamagablesInScene; // All idamageable should add themself to this list
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("dubble manager");
        }
        followerList = new List<Transform>();
        iDamagablesInScene = new List<IDamageable> ();
    }
    private void OnEnable()
    {//Refactor into a OnSceneObjectCreated
        BattleSceneActions.OnDamagableDestroyed += HandleDamagableDestroyed; // triggers every time someone with ITargatableByEnemy is created
        BattleSceneActions.OnDamagableCreated += HandleDamagableCreated; // triggers every time someone with ITargatableByEnemy is created
 
        BattleSceneActions.setInfluence += SetInfluence; // SET influence each turn Triggered by statemachine 

    }


    private void OnDisable()
    {
        BattleSceneActions.OnDamagableDestroyed -= HandleDamagableDestroyed;
        BattleSceneActions.OnDamagableCreated -= HandleDamagableCreated;

        BattleSceneActions.setInfluence -= SetInfluence;
    }
    private void SetInfluence(int obj)
    {
        influence =obj;
        BattleSceneActions.OnInfluenceChanged(influence);
    }
    public void AddInfluence(int amount)
    {
        influence += amount;
        BattleSceneActions.OnInfluenceChanged(influence);
    }
    private void RemoveFollowerFromList(Transform transform)
    {
        followerList.Remove(transform);
        BattleSceneActions.OnFollowerCountChanged(followerList.Count);
    }

    private void AddFollowerToList(Transform transform)
    {
        followerList.Add(transform);
        BattleSceneActions.OnFollowerCountChanged(followerList.Count);
    }

    private void HandleDamagableCreated(IDamageable target)
    {
        if(target.damageableType == iDamageableTypeEnum.follower)
        {
            AddFollowerToList(target.GetTransform());
        }
        Debug.Log("target added" + target.ToString());
        iDamagablesInScene.Add( target );
        
    }
    private void HandleDamagableDestroyed(IDamageable target)
    {
        if (target.damageableType == iDamageableTypeEnum.follower)
        {
            RemoveFollowerFromList(target.GetTransform());
        }
        iDamagablesInScene.Remove(target);
    }
}
