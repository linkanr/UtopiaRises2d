using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/Spawning")]
public class SoBattleSceneStateSpawningEnemies : BaseState<BattleSceneStateMachine>
{
    public override void OnStateEnter()
    {
        BattleSceneActions.OnPause(false);
        BattleSceneActions.OnStartSpawning?.Invoke();
        GameSceneRef.instance.inHandPile.gameObject.SetActive(false);
        EnemyManager.Instance.SetSpawning(true);
        BattleSceneActions.OnSpawnInterwallDone += OnSpawningDone;
    }

    private void OnSpawningDone()
    {
        stateMachine.SetState(typeof(SoBattleSceneStatePlayCards));
    }

    public override void OnStateExit()
    {
        EnemyManager.Instance.SetSpawning(false);
        BattleSceneActions.OnSpawnInterwallDone -= OnSpawningDone;
        BattleSceneActions.OnPause(true);
    }

    public override void OnStateUpdate()
    {
        // DO NOTHING
    }
}