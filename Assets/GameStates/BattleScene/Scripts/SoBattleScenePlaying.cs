using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/SpawningDone")]
public class SoBattleScenePlaying : BaseState<BattleSceneStateMachine>
{

    public override void OnStateEnter()
    {
        EnemyManager.Instance.SetSpawning(false);
        TimeActions.OnPause(false);



    }



    public override void OnObjectDestroyed()
    {

    }


    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
       
    }
}