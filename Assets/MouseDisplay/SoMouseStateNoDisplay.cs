using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/States/MouseStates/NoDisplay")]
public class SoMouseStateNoDisplay : BaseState<MouseDisplayStateMachine>
{
    public override void OnStateEnter()
    {
        stateMachine.TurnOfDisplay();
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}