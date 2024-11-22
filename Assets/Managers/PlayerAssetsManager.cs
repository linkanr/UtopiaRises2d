using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssetsManager : MonoBehaviour

{
    public static PlayerAssetsManager Instance;
    public List<Transform> followerList;
    public int influence  {get; private set;}
    public List<ITargetableByEnemy> iTargetableByEnememiesList; // All idamageable should add themself to this list
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
        iTargetableByEnememiesList = new List<ITargetableByEnemy> ();
    }
    private void OnEnable()
    {//Refactor into a OnSceneObjectCreated
        BattleSceneActions.OnTargetableDestroyed += RemoveTargable; // triggers every time someone with ITargatableByEnemy is created
        BattleSceneActions.OnTargetableCreated += AddTargetableToList; // triggers every time someone with ITargatableByEnemy is created
        BattleSceneActions.OnFollowerCreated += AddFollowerToList; //Triggered by follower adds to list
        BattleSceneActions.OnFollowerDestroyed += RemoveFollowerFromList; // remove follower from list
        BattleSceneActions.setInfluence += SetInfluence; // SET influence each turn Triggered by statemachine 

    }


    private void OnDisable()
    {
        BattleSceneActions.OnTargetableDestroyed -= RemoveTargable;
        BattleSceneActions.OnTargetableCreated -= AddTargetableToList;
        BattleSceneActions.OnFollowerCreated -= AddFollowerToList;
         BattleSceneActions.OnFollowerDestroyed += RemoveFollowerFromList;
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

    private void AddTargetableToList(ITargetableByEnemy target)
    {
        Debug.Log("target added" + target.ToString());
        iTargetableByEnememiesList.Add( target );
        
    }
    private void RemoveTargable(ITargetableByEnemy target)
    {
        iTargetableByEnememiesList.Remove(target);
    }
}
