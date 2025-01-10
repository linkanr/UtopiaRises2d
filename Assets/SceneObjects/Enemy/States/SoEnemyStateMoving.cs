using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "ScriptableObjects/States/Enemy/EnemyMoving")]
public class SoEnemeyStateMoving : BaseState<EnemyStateMachine>

{
    public override void OnStateEnter()
    {
        stateMachine.enemy.mover.Move(true);
        
        BattleSceneActions.GlobalTimeChanged += CheckIfTargetStillInRange;
    }

    private void CheckIfTargetStillInRange(BattleSceneTimeArgs args)
    {
        if (stateMachine.enemy.targeter.CheckIfTargetIsInDistance())
        {
           

            stateMachine.SetState(typeof(SoEnemyStateStopped));
            return;
        }
    }

    public override void OnStateExit()
    {
        BattleSceneActions.GlobalTimeChanged -= CheckIfTargetStillInRange;
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
        



        stateMachine.enemy.targeter.lookForNewTargetTime += BattleClock.Instance.deltaValue;
        if (stateMachine.enemy.targeter.lookForNewTargetTime > 2f)
        {
            stateMachine.enemy.targeter.lookForNewTargetTime = 0f;
            stateMachine.SetState(typeof (SoEnemyStateLookingForTarget));
        }
    }
}