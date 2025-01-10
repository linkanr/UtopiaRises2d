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
        
        if (stateMachine.shootingBuilding.targeter.target == null) 
        {
            //Debug.Log("looking for target");
            stateMachine.shootingBuilding.targeter.Seek();

        }
        else if (!stateMachine.shootingBuilding.targeter.target.IsValid()) // target is found but probobly dead set it to null
        {
            Debug.LogWarning("target is not null but not valid");
            stateMachine.shootingBuilding.targeter.RemoveTarget();
            
            return;
        }
        else 
        {
            stateMachine.SetState(typeof(SoShotingBuildingStateTargetFound));
        }
    }


}