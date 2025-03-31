using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/PlayCArds")]
public class SoBattleSceneStatePlayCards : BaseState<BattleSceneStateMachine>
{
    public Transform endTurnButtonTransform;

    public override void OnStateEnter()
    {
        BattleSceneActions.OnSpawningInterwallEnding();
        //Show end state button
        //Debug.Log("Entering play cards state");
        GameSceneRef.instance.inHandPile.gameObject.SetActive(true);//Change to a simple 
        BattleSceneActions.OnDrawCard(PlayerGlobalsManager.instance.cardAmount);
        BattleSceneActions.setInfluence(PlayerGlobalsManager.instance.influenceEachTurn);
        TimeActions.OnPause(true);
        Action unityAction = new Action(() => { EndTurn(); });
        ButtonWithDelegate.CreateThis(unityAction, GameSceneRef.instance.endTurnParent, "End Turn");
        EnemyManager.Instance.SetSpawning(false);
        

    }

    private void EndTurn()
    {
        BattleSceneActions.OnSpawningStarting();
        stateMachine.SetState(typeof(SoBattleSceneStateSpawningEnemies));
        AstarPath.active.Scan();
    }

    public override void OnStateExit()
    {
        if (GameSceneRef.instance.inHandPile != null)
            GameSceneRef.instance.inHandPile.gameObject.SetActive(false);
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnObjectDestroyed()
    {
        
    }
}