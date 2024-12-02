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

    private void StartGame()
    {
        stateMachine.SetState(typeof(SoLoadBattleScene));
    }

    public override void OnStateExit()
    {
       
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