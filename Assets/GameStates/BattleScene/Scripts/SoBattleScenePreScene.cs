using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/PreScene")]
public class SoBattleScenePreScene : BaseState<BattleSceneStateMachine>

{
    public override void OnStateEnter()
    {
        BattleSceneActions.OnPreSceneDone += MoveOn;
        BattleSceneActions.OnPreSceneInit();


    }

    private void MoveOn()
    {
        stateMachine.SetState(typeof(SoBattleSceneStateSceneStarting));
    }

    public override void OnStateExit()
    {
        BattleSceneActions.OnPreSceneDone -= MoveOn;

    }

    public override void OnStateUpdate()
    {
        // DO NOTHING
    }
}