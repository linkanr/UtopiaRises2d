using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/SpawningDone")]
public class SoBattleSceneStateSpawningDone : BaseState<BattleSceneStateMachine>
{
    public override void OnStateEnter()
    {
        EnemyManager.Instance.SetSpawning(false);
        TimeActions.OnPause(true);


    }



    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
       
    }
}