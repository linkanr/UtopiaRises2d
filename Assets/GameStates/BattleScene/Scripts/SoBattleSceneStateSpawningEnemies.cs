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
        BattleSceneActions.OnAllEnemiesSpawned += AllEnemiesSpawned;
    }

    private void AllEnemiesSpawned()
    {
        stateMachine.SetState(typeof(SoBattleSceneStateSpawningDone));
    }

    public override void OnStateExit()
    {
        BattleSceneActions.OnAllEnemiesSpawned -= AllEnemiesSpawned;
    }

    public override void OnStateUpdate()
    {
        // DO NOTHING
    }
}