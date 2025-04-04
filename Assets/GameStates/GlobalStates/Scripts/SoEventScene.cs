using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/States/GlobalStates/EventScene")]
public class SoEventScene : BaseState<GameStateMachine>
{
    public override void OnStateEnter()
    {
        stateMachine.StartCoroutine(EventSceneLoaded());
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        // DO nothing
    }
    public override void OnObjectDestroyed()
    {

    }
    public IEnumerator EventSceneLoaded()
    {

        yield return null;
        GlobalActions.OnEventSceneLoaded();
    }

}