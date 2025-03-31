using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/Spawning")]
public class SoBattleSceneStateSpawningEnemies : BaseState<BattleSceneStateMachine>
{
    public override void OnStateEnter()
    {

        TimeActions.OnPause(false);
        BattleSceneActions.OnSpawningStarting?.Invoke();
        GameSceneRef.instance.inHandPile.gameObject.SetActive(false);
        EnemyManager.Instance.SetSpawning(true);


;
     
    }
    public override void OnObjectDestroyed()
    {
        
    }



    public override void OnStateExit()
    {
        EnemyManager.Instance.SetSpawning(false);


    }

    public override void OnStateUpdate()
    {
        // DO NOTHING
    }
}