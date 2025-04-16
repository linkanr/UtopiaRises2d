using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/Enemy/EnemyLookingForTarget")]
public class SoEnemyStateLookingForTarget : BaseState<EnemyStateMachine>
{
    public override void OnObjectDestroyed()
    {
        
    }

    public override void OnStateEnter()
    {
        Seek();
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
       // Debug.Log("Seeking");
        if (stateMachine.enemy.targeter.target == null)
        {
         //   Debug.Log("No target");
            stateMachine.enemy.targeter.GetSeeker().Seek(stateMachine.enemy.transform.position, stateMachine.enemy.targeter.possibleTargetTypes,stateMachine.enemy.targeter,moverComponent:stateMachine.enemy.mover);
        }
        else if (!stateMachine.enemy.targeter.target.IsValid())
        {
           // Debug.Log("No valid taget target");
            stateMachine.enemy.targeter.GetSeeker().Seek(stateMachine.enemy.transform.position, stateMachine.enemy.targeter.possibleTargetTypes, stateMachine.enemy.targeter);

        }
        else
        {
            //Debug.Log("valid taget target Movings");
            stateMachine.SetState(typeof(SoEnemeyStateMoving));
        }
    }
}