using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/States/GlobalStates/PostBattle")]
public class SoPostBattle : BaseState<GameStateMachine>
{
    public override void OnStateEnter()
    {
        GlobalActions.OnPostBattle(); 
    }



    public override void OnStateExit()
    {
       
    }

    public override void OnStateUpdate()
    {
        //
    }
}