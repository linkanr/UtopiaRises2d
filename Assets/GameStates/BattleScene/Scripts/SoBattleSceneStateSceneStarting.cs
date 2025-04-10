using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/BattleStaes/SceneStarting")]
public class SoBattleSceneStateSceneStarting : BaseState<BattleSceneStateMachine>
{
    public override void OnStateEnter()
    {
        Debug.Log("Entering scene SoBattleSceneStateSceneStarting  state");
        if (!CardManager.instance.intialized)
        {
            CardManager.instance.AddStartingCardsToDeck();
         
        }

        BattleSceneActions.OnInitializeScene();
        TagFromLayerZ.instance.UpdateGraphAndTags();

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