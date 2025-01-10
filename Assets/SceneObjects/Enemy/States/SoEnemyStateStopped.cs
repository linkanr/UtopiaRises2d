using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/Enemy/EnemyStopped")]
public class SoEnemyStateStopped : BaseState<EnemyStateMachine>
{
    public override void OnStateEnter()
    {
        Debug.Log("entered stopped state");
        stateMachine.enemy.mover.Move(false);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        if (stateMachine.enemy.targeter.target == null)
        {
            stateMachine.SetState(typeof(SoEnemyStateLookingForTarget));
            return;
        }
            
        if (stateMachine.enemy.targeter.target.IsValid())
        {
            
            stateMachine.enemy.targeter.attackTimer += BattleClock.Instance.deltaValue;
            if (stateMachine.enemy.targeter.attackTimer > stateMachine.enemy.GetStats().reloadTime)
            {
                stateMachine.enemy.targeter.soAttackSystem.Attack(stateMachine.enemy.targeter, stateMachine.enemy.targeter.target, stateMachine.enemy.GetStats().damage);
                stateMachine.enemy.targeter.attackTimer = 0f;
            }
        }
        else
        {
            stateMachine.SetState(typeof(SoEnemyStateLookingForTarget));
        }

    }
}
