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



;
     
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