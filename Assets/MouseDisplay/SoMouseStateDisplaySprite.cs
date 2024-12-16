using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/MouseStates/Display")]
public class SoMouseStateDisplaySprite : BaseState<MouseDisplayStateMachine>
{
    public override void OnStateEnter()
    {
        stateMachine.TurnOnDisplay();
 
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        stateMachine.spriteGO.transform.position = GridCellManager.Instance.gridConstrution.GetCurrentCellPostionByMouse();
       
    }
}