using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/GlobalStates/SpoilsScene")]
public class SoSpoilsScne : BaseState<GameStateMachine>
{
    public override void OnStateEnter()
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
    public override void OnObjectDestroyed()
    {

    }
}