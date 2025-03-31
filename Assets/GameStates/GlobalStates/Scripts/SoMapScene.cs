using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/States/GlobalStates/MapScene")]
public class SoMapScene : BaseState<GameStateMachine>
{
    public override void OnStateEnter()
    {
        GlobalActions.OnMapSceneEntered();
    }

    public override void OnStateExit()
    {
        GlobalActions.OnMapSceneExited();
    }

    public override void OnStateUpdate()
    {
        // DO nothing
    }


    public override void OnObjectDestroyed()
    {

    }
}
