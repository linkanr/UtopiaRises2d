using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/States/Enemy/EnemyStopped")]
public class SoEnemyStateStopped : BaseState<EnemyStateMachine>
{
    public override void OnStateEnter()
    {
        Debug.Log("entered stopped state");
        stateMachine.enemy.mover.StateAllowsMovement(false);
    }

    public override void OnStateExit()
    {
        
    }
    public override void OnObjectDestroyed()
    {

    }
    public override void OnStateUpdate()
    {
        if (stateMachine.enemy.targeter.target == null)
        {
            Debug.Log("No target found, switching to LookingForTarget state.");
            stateMachine.SetState(typeof(SoEnemyStateLookingForTarget));
            return;
        }

        if (stateMachine.enemy.targeter.target.IsValid())
        {
            stateMachine.enemy.targeter.attackTimer += BattleClock.Instance.deltaValue;
           // Debug.Log($"Attack timer updated: {stateMachine.enemy.targeter.attackTimer}");
            if (stateMachine.enemy.targeter.attackTimer > stateMachine.enemy.GetStats().reloadTime)
            {
                Debug.Log("Attack timer exceeded reload time, performing attack.");
                stateMachine.enemy.targeter.soAttackSystem.Attack(stateMachine.enemy);
                stateMachine.enemy.targeter.attackTimer = 0f;
            }
        }
        else
        {
            Debug.Log("Target is not valid, switching to LookingForTarget state.");
            stateMachine.SetState(typeof(SoEnemyStateLookingForTarget));
        }
    }
}
