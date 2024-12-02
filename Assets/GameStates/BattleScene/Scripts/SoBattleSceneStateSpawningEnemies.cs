using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/Spawning")]
public class SoBattleSceneStateSpawningEnemies : BaseState<BattleSceneStateMachine>
{
    public override void OnStateEnter()
    {
        BattleSceneActions.OnPause(false);
        GameSceneRef.instance.panel.gameObject.SetActive(false);
        EnemyManager.Instance.SetSpawning(true);

    }


    public override void OnStateExit()
    {
       
    }

    public override void OnStateUpdate()
    {
        // DO NOTHING
    }
}