using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/Spawning")]
public class SoBattleSceneStateSpawningEnemies : BaseState<BattleSceneStateMachine>
{
    public override void OnStateEnter()
    {
        AstarPath.active.Scan();
        TimeActions.OnPause(false);
        BattleSceneActions.OnLiveStatsStarting?.Invoke();
        GameSceneRef.instance.inHandPile.gameObject.SetActive(false);
        EnemyManager.Instance.SetSpawning(true);
        BattleSceneActions.OnSpawnInterwallDone += OnSpawningDone;
    }
    public override void OnObjectDestroyed()
    {
        BattleSceneActions.OnSpawnInterwallDone -= OnSpawningDone;
    }

    private void OnSpawningDone()
    {
        stateMachine.SetState(typeof(SoBattleSceneStatePlayCards));
    }

    public override void OnStateExit()
    {
        EnemyManager.Instance.SetSpawning(false);
        BattleSceneActions.OnSpawnInterwallDone -= OnSpawningDone;
        TimeActions.OnPause(true);
    }

    public override void OnStateUpdate()
    {
        // DO NOTHING
    }
}