using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
    public override void OnObjectDestroyed()
    {

    }
    public override void OnStateUpdate()
    {
        stateMachine.shootingBuilding.targeter.attackTimer += BattleClock.Instance.deltaValue;


        if (stateMachine.shootingBuilding.targeter.target == null)
        {
            stateMachine.SetState(typeof(SoShotingBuildingStateLookingForTarget));
            return;


        }
        float targetDistance = Vector3.Distance(stateMachine.shootingBuilding.transform.position, stateMachine.shootingBuilding.targeter.target.targetTransform.position);
        if (targetDistance > stateMachine.shootingBuilding.GetStats().maxRange)
        {
            stateMachine.shootingBuilding.targeter.RemoveTarget();
            stateMachine.SetState(typeof(SoShotingBuildingStateLookingForTarget));
            return;
        }
        else if (!stateMachine.shootingBuilding.targeter.target.IsValid()) //If target is not null but not valid neither
        {
            stateMachine.shootingBuilding.targeter.RemoveTarget();
            stateMachine.SetState(typeof(SoShotingBuildingStateLookingForTarget));
            return;
        }
        else if (stateMachine.shootingBuilding.targeter.attackTimer > stateMachine.shootingBuilding.GetStats().reloadTime) // This happens when target is existing and valid
        {

            stateMachine.shootingBuilding.targeter.soAttackSystem.Attack(stateMachine.shootingBuilding);

            stateMachine.shootingBuilding.targeter.attackTimer = 0f;
        }

   
      



    }
}