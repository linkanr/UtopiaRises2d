using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectManager : MonoBehaviour

{
    public static SceneObjectManager Instance;
    public List<Transform> followerList;
    public SceneObjectGetter sceneObjectGetter;
    public int influence  {get; private set;}
    public List<SceneObject> sceneObjectsInScene; // All idamageable should add themself to this list
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
        sceneObjectsInScene = new List<SceneObject> ();
        sceneObjectGetter = new SceneObjectGetter(this);
    }
    private void OnEnable()
    {//Refactor into a OnSceneObjectCreated
        BattleSceneActions.OnSceneObjectDestroyed += HandleDamagableDestroyed; // triggers every time someone with ITargatableByEnemy is created
        BattleSceneActions.OnSceneObejctCreated += HandleDamagableCreated; // triggers every time someone with ITargatableByEnemy is created
 
        BattleSceneActions.setInfluence += SetInfluence; // SET influence each turn Triggered by statemachine 

    }


    private void OnDisable()
    {
        BattleSceneActions.OnSceneObjectDestroyed -= HandleDamagableDestroyed;
        BattleSceneActions.OnSceneObejctCreated -= HandleDamagableCreated;

        BattleSceneActions.setInfluence -= SetInfluence;
    }
    //Influece should be moved to a player asset manager

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

    private void HandleDamagableCreated(SceneObject target)
    {
        if(target.GetStats().sceneObjectType == SceneObjectTypeEnum.follower)
        {
            AddFollowerToList(target.transform);
        }
        //Debug.Log("target added" + target.ToString());
        sceneObjectsInScene.Add( target );
        
    }
    private void HandleDamagableDestroyed(SceneObject target)
    {
        if (target.GetStats().sceneObjectType == SceneObjectTypeEnum.follower)
        {
            RemoveFollowerFromList(target.transform);
        }
        sceneObjectsInScene.Remove(target);
    }
}
