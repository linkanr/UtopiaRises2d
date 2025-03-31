using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/States/GlobalStates/BattleScene")]
public class SoBattleScene : BaseState<GameStateMachine>
{
    public override void OnStateEnter()
    {
        stateMachine.StartCoroutine(BattleSceneLoaded());
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
    public IEnumerator BattleSceneLoaded()
    {
        
        yield return null;
        GlobalActions.OnBattleSceneLoaded();
    }

}