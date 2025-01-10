using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/PreScene")]
public class SoBattleScenePreScene : BaseState<BattleSceneStateMachine>

{
    public override void OnStateEnter()
    {
        PlayerGlobalsManager.instance.soPlayerBaseBuilding.Init(PlayerGlobalsManager.instance.basePositions);
        CardManager.Instance.GetStartingCards();
        BattleSceneActions.setInfluence(3);
        stateMachine.SetState(typeof(SoBattleSceneStateSceneStarting));



    }



 
    public override void OnStateExit()
    {
     

    }

    public override void OnStateUpdate()
    {
        // DO NOTHING
    }
}