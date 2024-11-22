using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/building/Shotingtower/LookingForTarget")]
public class SoShotingBuildingStateLookingForTarget : BaseState<ShotingBuildingStateMachine>
{

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        if (stateMachine.shootingBuilding.targetsTransform == null) 
        {
            //Debug.Log("looking for target");
            stateMachine.shootingBuilding.LookForTarget();

        }
        else
        {
            stateMachine.SetState(typeof(SoShotingBuildingStateTargetFound));
        }
    }


}