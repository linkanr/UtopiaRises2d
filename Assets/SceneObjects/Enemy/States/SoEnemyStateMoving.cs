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
        TimeActions.OnSecondChange += CheckForCloserTarget;
    }

    private void CheckForCloserTarget()
    {
        Debug.Assert(stateMachine != null, "StateMachine is null");
        Debug.Assert(stateMachine.enemy != null, "Enemy is null");
        Debug.Assert(stateMachine.enemy.idamageableComponent != null, "IDamageableComponent is null");
        Debug.Assert(stateMachine.enemy.targeter != null, "Targeter is null");
        Debug.Assert(stateMachine.enemy.mover != null, "Mover is null");

        var seeker = stateMachine.enemy.targeter.GetSeeker();
        Debug.Assert(seeker != null, "Seeker is null");

        var damageableTransform = stateMachine.enemy.idamageableComponent.GetTransform();
        if (damageableTransform == null)
        {
            Debug.LogWarning("No damageable transform");
            return;
        }
        Debug.Assert(damageableTransform != null, "Damageable Transform is null");

        // Perform the Seek operation
        seeker.Seek(damageableTransform.position, stateMachine.enemy.targeter.possibleTargetTypes, stateMachine.enemy.targeter, moverComponent: stateMachine.enemy.mover);
    }


    public override void OnStateExit()
    {
        TimeActions.OnSecondChange -= CheckForCloserTarget;
    }

    public override void OnStateUpdate()
    {
        if (stateMachine.enemy.targeter.target == null)
        {
            Debug.Log("no target");
            stateMachine.SetState(typeof(SoEnemyStateLookingForTarget));
            return;
        }
        if (!stateMachine.enemy.targeter.target.IsValid())
        {
            Debug.Log("no valid target");
            stateMachine.SetState(typeof(SoEnemyStateLookingForTarget));
            return;
        }


    }

    public override void OnObjectDestroyed()
    {
        TimeActions.OnSecondChange -= CheckForCloserTarget;
    }
}
