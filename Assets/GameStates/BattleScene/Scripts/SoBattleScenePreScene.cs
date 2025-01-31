using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/PreScene")]
public class SoBattleScenePreScene : BaseState<BattleSceneStateMachine>

{
    public override void OnStateEnter()
    {
        PlayerGlobalsManager.instance.soPlayerBaseBuilding.Init(PlayerGlobalsManager.instance.basePositions);
        
        BattleSceneActions.setInfluence(3);
        stateMachine.SetState(typeof(SoBattleSceneStateSceneStarting));
        Instantiate(Resources.Load("mouseDisplayManager") as GameObject,stateMachine.transform);



    }
    public override void OnObjectDestroyed()
    {

    }




    public override void OnStateExit()
    {
     
        
    }

    public override void OnStateUpdate()
    {
        // DO NOTHING
    }
}