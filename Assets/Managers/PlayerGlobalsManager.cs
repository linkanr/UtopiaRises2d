using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalsManager : MonoBehaviour
{
    public SoPlayerBaseBuilding soPlayerBaseBuilding;
    public static PlayerGlobalsManager instance;
    public Vector3 basePositions;
    public int influence { get; private set; }
    public List<Transform> followerList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("two global managers");
        }
        followerList = new List<Transform>();
    }

    private void OnEnable()
    {
        BattleSceneActions.OnSceneObjectDestroyed += HandleDamagableDestroyed; // triggers every time someone with ITargatableByEnemy is created
        BattleSceneActions.OnSceneObejctCreated += HandleDamagableCreated;
        BattleSceneActions.setInfluence += SetInfluence; // SET influence each turn Triggered by statemachine 
    }
    private void OnDisable()
    {
        BattleSceneActions.OnSceneObjectDestroyed += HandleDamagableDestroyed; // triggers every time someone with ITargatableByEnemy is created
        BattleSceneActions.OnSceneObejctCreated += HandleDamagableCreated;
        BattleSceneActions.setInfluence -= SetInfluence;
    }
    public int influenceEachTurn { get; private set; }
    public int cardAmount { get; private set; }
    private void Start()
    {

        influenceEachTurn = 3;
        cardAmount = 5;
    }
    private void SetInfluence(int obj)
    {
        influence = obj;
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
        if (target.GetStats().sceneObjectType == SceneObjectTypeEnum.follower)
        {
            AddFollowerToList(target.transform);
        }
    }
    private void HandleDamagableDestroyed(SceneObject target)
    {
        if (target.GetStats().sceneObjectType == SceneObjectTypeEnum.follower)
        {
            RemoveFollowerFromList(target.transform);
        }
    }
}
