using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/States/GlobalStates/StartScene")]
public class SoStartScreen : BaseState<GameStateMachine>
{
    public override void OnStateEnter()
    {
        GlobalActions.OnClickStartGame += StartGame;
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            stateMachine.StartCoroutine(SkipStartScreen());
            
        }
    }
    public override void OnObjectDestroyed()
    {

        GlobalActions.OnClickStartGame-= StartGame;
    }

    private void StartGame()
    {
        stateMachine.SetState(typeof(SoLoadMapScene));
    }


    public override void OnStateExit()
    {
        GlobalActions.OnClickStartGame -= StartGame;
    }

    public override void OnStateUpdate()
    {
        // DO nothing
    }

    public IEnumerator SkipStartScreen()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.SetState(typeof(SoBattleScene));
    }

}