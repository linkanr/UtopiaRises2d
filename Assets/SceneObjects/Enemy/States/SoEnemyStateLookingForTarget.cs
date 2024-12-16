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
        if (stateMachine.enemy.target == null)
        {
            stateMachine.enemy.enemySeekSystem.Seek(stateMachine.enemy.GetTransform().position, stateMachine.enemy.possibleTargetTypes);
        }
        else if (!stateMachine.enemy.target.IsValid())
        {
            
            stateMachine.enemy.enemySeekSystem.Seek(stateMachine.enemy.GetTransform().position,stateMachine.enemy.possibleTargetTypes);

        }
        else
        {
            stateMachine.SetState(typeof(SoEnemeyStateMoving));
        }

    }
}