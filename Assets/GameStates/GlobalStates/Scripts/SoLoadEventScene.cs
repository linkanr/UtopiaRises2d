using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/States/GlobalStates/LoadEventScene")]
public class SoLoadEventScene : BaseState<GameStateMachine>
{
    public override void OnStateEnter()
    {
        stateMachine.StartCoroutine(LoadScene("RandomEvent"));

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

        stateMachine.SetState(typeof(SoEventScene));
    }
    public IEnumerator ActualLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(sceneName).isLoaded);

        yield return new WaitForEndOfFrame();
    }
    public override void OnObjectDestroyed()
    {

    }
}
