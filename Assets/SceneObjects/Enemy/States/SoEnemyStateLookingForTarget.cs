using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/Enemy/EnemyLookingForTarget")]
public class SoEnemyStateLookingForTarget : BaseState<EnemyStateMachine>
{
    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {

        Seek();

    }


    private void Seek()
    {
        if (stateMachine.enemy.targeter.target == null)
        {
            stateMachine.enemy.targeter.GetSeeker().Seek(stateMachine.enemy.idamageableComponent.GetTransform().position, stateMachine.enemy.targeter.possibleTargetTypes,stateMachine.enemy.targeter,seekStyle:SeekStyle.findRoute,moverComponent:stateMachine.enemy.mover);
        }
        else if (!stateMachine.enemy.targeter.target.IsValid())
        {

            stateMachine.enemy.targeter.GetSeeker().Seek(stateMachine.enemy.idamageableComponent.GetTransform().position, stateMachine.enemy.targeter.possibleTargetTypes, stateMachine.enemy.targeter);

        }
        else
        {
            stateMachine.SetState(typeof(SoEnemeyStateMoving));
        }
    }
}