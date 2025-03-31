using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/MouseStates/Display")]
public class SoMouseStateDisplaySprite : BaseState<MouseDisplayStateMachine>
{
    public override void OnObjectDestroyed()
    {
        
    }

    public override void OnStateEnter()
    {
        stateMachine.TurnOnDisplay();
 
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        if (GridCellManager.instance != null)
        {
            stateMachine.spriteGO.transform.position = GridCellManager.instance.gridConstrution.GetCurrentCellPostionByMouse();


        }
    }
}