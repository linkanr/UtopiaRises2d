using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/SceneStarting")]
public class SoBattleSceneStateSceneStarting : BaseState<BattleSceneStateMachine>
{
    public override void OnStateEnter()
    {

        BattleSceneActions.OnInitializeScene();
        AstarPath.active.Scan();
        stateMachine.SetState(typeof(SoBattleSceneStatePlayCards));
    }
    public override void OnObjectDestroyed()
    {

    }

    public override void OnStateExit()
    {
        BattleSceneActions.OnIntializationComplete();
    }

    public override void OnStateUpdate()
    {
        // DO NOTHING
    }
}