using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/States/GlobalStates/BattleScene")]
public class SoBattleScene : BaseState<GameStateMachine>
{
    public override void OnStateEnter()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public override void OnStateExit()
    {
        //Go to next state
    }

    public override void OnStateUpdate()
    {
        // DO nothing
    }
}