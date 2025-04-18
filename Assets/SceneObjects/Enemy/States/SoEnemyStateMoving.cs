using System;

using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "ScriptableObjects/States/Enemy/EnemyMoving")]
public class SoEnemeyStateMoving : BaseState<EnemyStateMachine>
{
    public override void OnStateEnter()
    {
        stateMachine.enemy.mover.StateAllowsMovement(true);
        
    }






    public override void OnStateUpdate()
    {
        if (stateMachine.enemy.targeter.target == null)
        {
           
            stateMachine.SetState(typeof(SoEnemyStateLookingForTarget));
            return;
        }
        if (!stateMachine.enemy.targeter.target.IsValid())
        {
            stateMachine.SetState(typeof(SoEnemyStateLookingForTarget));
            return;
        }
        if (stateMachine.enemy.targeter.target.IsValid())
        {
            if (Vector3.Distance(stateMachine.enemy.targeter.target.targetTransform.position, stateMachine.enemy.transform.position) < .5f)
            {
                stateMachine.enemy.targeter.soAttackSystem.Attack(stateMachine.enemy);
            }

        }



    }

    public override void OnObjectDestroyed()
    {
       
    }

    public override void OnStateExit()
    {
        
    }
}
