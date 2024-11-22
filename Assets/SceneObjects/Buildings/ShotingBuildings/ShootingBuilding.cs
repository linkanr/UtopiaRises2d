using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;
/// <summary>
/// Based on buidling but add functionality with the shoting building state machince and IcanShot interface
/// </summary>
public class ShootingBuilding : Building, ICanShoot, IHasLifeSpan
{
    
    public float timer;
    
    
    public Transform shotingPos;
    
    
    public IDamageable target { get; private set; }


    public Transform targetsTransform;
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
            


        }
        if (target != null && timer > stats.GetValue<float>(StatsInfoTypeEnum.reloadTime))
        {
            //Debug.Log("fire");
            stats.GetValue<VisualEffect>(StatsInfoTypeEnum.fireEffect).Play();
            target.TakeDamage(stats.GetValue<int>(StatsInfoTypeEnum.damageAmount));
            
            timer = 0f;
        }
    }

    public bool LookForTarget()// calls the manager and check the looker class uses the type to get the corret info
    {
       IDamageable newTarget = EnemyManager.Instance.looker.LookForTarget(stats.GetLookForEnemyType(), shotingPos.position, stats.GetFloat(StatsInfoTypeEnum.maxShotingDistance));
        if (newTarget != null) 
        {
            
            target = newTarget;
            target.OnDeath += RemoveTarget;
            targetsTransform = newTarget.GetTransform();
            return true;
        }
        return false;
        
    }

    private void RemoveTarget(object sender, OnDeathArgs e)
    {
        
        if (target == e.damageable)
        {
            target.OnDeath -= RemoveTarget;
            target = null;
        }
        
    }





    public TimeStruct getBirthLifeSpan()
    {
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
        DestroySceneObject();
    }

 




}
public enum LookForEnemyType
{
    closest,
    mostHelth,
    furthest,
    leastHealth
}