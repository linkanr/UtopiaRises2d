using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "ScriptableObjects/States/Enemy/EnemyMoving")]
public class SoEnemeyStateMoving : BaseState<EnemyStateMachine>

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
            stateMachine.SetState(typeof(SoEnemyStateLookingForTarget));
            return;
        }
        if (!stateMachine.enemy.target.IsValid())
        {
            stateMachine.SetState(typeof(SoEnemyStateLookingForTarget));
            return;
        }
        
        if (  stateMachine.enemy.CheckIfTargetIsInDistance())
        {
            Debug.Log("reached path");
            stateMachine.SetState(typeof(SoEnemyStateStopped));
            return;
        }


        stateMachine.enemy.lookForNewTargetTime += Time.deltaTime;
        if (stateMachine.enemy.lookForNewTargetTime > stateMachine.enemy.lookForNewMTargetMaxTime)
        {
            stateMachine.enemy.enemySeekSystem.Seek(stateMachine.enemy.transform.position, stateMachine.enemy.possibleTargetTypes);
            stateMachine.enemy.lookForNewTargetTime = 0;
        }
    }
}