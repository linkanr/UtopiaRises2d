using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/SceneStarting")]
public class SoBattleSceneStateSceneStarting : BaseState<BattleSceneStateMachine>
{
    public override void OnStateEnter()
    {

        BattleSceneActions.OnInitializeScene();
        stateMachine.SetState(typeof(SoBattleSceneStateSpawningEnemies));
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        // DO NOTHING
    }
}