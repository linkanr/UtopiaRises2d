using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/States/GlobalStates/LoadMapScen")]
public class SoLoadMapScene : BaseState<GameStateMachine>
{
    public override void OnStateEnter()
    {

        stateMachine.StartCoroutine(LoadScene("Map"));

    }
    public override void OnObjectDestroyed()
    {

    }
    public override void OnStateExit()
    {
        //Go to next state
    }

    public override void OnStateUpdate()
    {
        // DO nothing
    }

    public IEnumerator LoadScene(string sceneName)
    {
        yield return ActualLoad(sceneName);

        stateMachine.SetState(typeof(SoMapScene));
    }
    public IEnumerator ActualLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(sceneName).isLoaded);

        yield return new WaitForEndOfFrame();
    }
}
