using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/building/Shotingtower/TargetFound")]

public class SoShotingBuildingStateTargetFound : BaseState<ShotingBuildingStateMachine>
{
    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        if (stateMachine.shootingBuilding.target == null || stateMachine.shootingBuilding.target.Equals(null))
        {
            //Debug.Log("setting to looking for target");
            stateMachine.SetState(typeof(SoShotingBuildingStateLookingForTarget));
        }
        else
        {
            if (DebuggerGlobal.instance.drawTargetLines)
            {
                DebuggerGlobal.instance.DrawLine(stateMachine.shootingBuilding.transform.position, stateMachine.shootingBuilding.target.transform.position);
            }
           
           // Debug.Log("not null" + stateMachine.shootingBuilding.target.ToString());
        }
   
      



    }
}