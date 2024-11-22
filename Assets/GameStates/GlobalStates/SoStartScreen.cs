using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/States/GlobalStates/StartScene")]
public class SoStartScreen : BaseState<GameStateMachine>
{
    public override void OnStateEnter()
    {
        GlobalActions.StartGame += StartGame;
    }

    private void StartGame()
    {
        stateMachine.SetState(typeof(SoBattleScene));
    }

    public override void OnStateExit()
    {
       
    }

    public override void OnStateUpdate()
    {
        // DO nothing
    }
}