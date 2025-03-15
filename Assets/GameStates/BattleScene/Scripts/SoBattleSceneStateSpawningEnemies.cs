using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/Spawning")]
public class SoBattleSceneStateSpawningEnemies : BaseState<BattleSceneStateMachine>
{
    public override void OnStateEnter()
    {
        AstarPath.active.Scan();
        TimeActions.OnPause(false);
        BattleSceneActions.OnSpawningStarting?.Invoke();
        GameSceneRef.instance.inHandPile.gameObject.SetActive(false);
        if (EnemyManager.Instance.EnemyCount().enemiesInNextWaves > 0)
        {
            Debug.Log("Eneter state: Spawning enemies");
            EnemyManager.Instance.SetSpawning(true);
        }
        else
        {
            Debug.Log("Eneter state: NOT Spawning enemies");
            EnemyManager.Instance.SetSpawning(false);
        }
;
        BattleSceneActions.OnSpawnInterwallDone += OnSpawningWaveDone;
    }
    public override void OnObjectDestroyed()
    {
        BattleSceneActions.OnSpawnInterwallDone -= OnSpawningWaveDone;
    }

    private void OnSpawningWaveDone()
    {
        stateMachine.SetState(typeof(SoBattleSceneStatePlayCards));
    }

    public override void OnStateExit()
    {
        EnemyManager.Instance.SetSpawning(false);
        BattleSceneActions.OnSpawnInterwallDone -= OnSpawningWaveDone;
        TimeActions.OnPause(true);
    }

    public override void OnStateUpdate()
    {
        // DO NOTHING
    }
}