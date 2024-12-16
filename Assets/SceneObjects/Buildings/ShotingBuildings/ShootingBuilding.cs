using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;
/// <summary>
/// Based on buidling but add functionality with the shoting building state machince and IcanShot interface
/// </summary>
public class ShootingBuilding : Building, IHasLifeSpan, ICanAttack
{
    
    public float timer;
    public Transform shotingPos; 
    public Target target { get; set; }
    public SceneObject attacker { get { return this; } set { value = this; } }

    public ShotingBuildingStateMachine stateMachine;
    private TimeLimterSceneObject timeLimiter;
    


    protected override void Start()
    {
        base.Start();
        stateMachine = GetComponent<ShotingBuildingStateMachine>();
        SetTimeLimiter();
       
    }

    protected virtual void Update()
    {
        
        timer += Time.deltaTime;


        if (target == null)
        {
            stateMachine.SetState(typeof(SoShotingBuildingStateLookingForTarget));
            return;


        }
        else if (!target.IsValid()) //If target is not null but not valid neither
        {
            stateMachine.SetState(typeof(SoShotingBuildingStateLookingForTarget));
            return;
        }
        else if (timer > stats.GetReloadTime()) // This happens when target is existing and valid
        {

            stats.GetSoAttackSystem().Attack(this, target);
            
            timer = 0f;
        }
    }

    public void LookForTarget()// calls the manager and check the looker class uses the type to get the corret info
    {
        IDamageable newTarget = EnemyManager.Instance.looker.LookForTarget(stats.GetLookForEnemyType(), shotingPos.position, stats.GetMaxShotingDistance());
        if (newTarget != null)
        {
            target = new Target(newTarget, this);
        }
        





    }







    public TimeStruct getBirthLifeSpan()

    {
        Debug.Log(stats.ToString());
        return TimeCalc.TimeToTimeStruct(stats.GetFloat(StatsInfoTypeEnum.lifeTime));
    }

    public void SetTimeLimiter()
    {
         
        timeLimiter = gameObject.AddComponent<TimeLimterSceneObject>();
        timeLimiter.Init(this);
    }
    public TimeLimterSceneObject GetTimeLimiter()
    {
       return timeLimiter;
    }


    public void OnLifeUp()
    {
        if (!isDead)
        {
            Die();
        }
        
    }

    public void RemoveTarget(object sender, IdamageAbleArgs e)
    {
        throw new NotImplementedException();
    }
}
public enum TargetPriorityEnum
{
    closest,    mostHelth,
    furthest,
    leastHealth,
    enemyBase,
    notRelevant
}