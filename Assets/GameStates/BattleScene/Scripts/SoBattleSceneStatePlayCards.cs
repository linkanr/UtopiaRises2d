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
        //Show end state button
        GameSceneRef.instance.panel.gameObject.SetActive(true);
        BattleSceneActions.OnDrawCard(PlayerGlobalsManager.Instance.cardAmount);
        BattleSceneActions.setInfluence(PlayerGlobalsManager.Instance.influenceEachTurn); 
        BattleSceneActions.OnPause(true);
        Action unityAction = new Action(() => { EndTurn(); });
        ButtonWithDelegate.CreateThis(unityAction, GameSceneRef.instance.endTurnParent, "End Turn");



    }

    private void EndTurn()
    {
        stateMachine.SetState(typeof(SoBattleSceneStateSpawningEnemies));
    
    }

    public override void OnStateExit()
    {
       
    }

    public override void OnStateUpdate()
    {
        
    }
}