using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/Enemy/EnemyStopped")]
public class SoEnemyStateStopped : BaseState<EnemyStateMachine>
{
    public override void OnStateEnter()
    {
        Debug.Log("entered stopped state");
        //stateMachine.enemy.aIPath.isStopped = true;
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        if (stateMachine.enemy.target.IsValid())
        {
            
            stateMachine.enemy.attackTimer += Time.deltaTime;
            if (stateMachine.enemy.attackTimer > stateMachine.enemy.enemyAttackSystem.attackTimerMax)
            {
                stateMachine.enemy.enemyAttackSystem.Attack(stateMachine.enemy, stateMachine.enemy.target);
                stateMachine.enemy.attackTimer = 0f;
            }
        }
        else
        {
            stateMachine.SetState(typeof(SoEnemyStateLookingForTarget));
        }

    }
}
