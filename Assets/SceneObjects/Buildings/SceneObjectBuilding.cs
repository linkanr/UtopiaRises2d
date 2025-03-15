using System;
using Pathfinding;
using UnityEngine;
[RequireComponent(typeof(TimeHealthSystem))]

public abstract class SceneObjectBuilding : StaticSceneObject, IDamageAble, IHasLifeSpan
{


    

   


    public IDamagableComponent iDamageableComponent { get ; set ; }

    protected override void Awake()
    {
        base.Awake();
        
    }
    protected override void Start()
    {
        base.Start();



    }
        #region HasLifeSpan
    public TimeStruct getBirthLifeSpan()

    {
        
        return TimeCalc.TimeToTimeStruct(GetStatsHandler().GetStats().GetFloat(StatsInfoTypeEnum.lifeTime));
    }


    public TimeTickerForIHasLifeSpan GetTimeLimiter()
    {
        return (iDamageableComponent as IDamagableTimeComponent).healthSystem.timeTicker ;
    }



    public void OnLifeUp()
    {
        if (!isDead)
        {
            isDead = true;
            OnSceneObjectDestroyedBase();
        }

    }
    #endregion
    protected override void OnObjectDestroyedObjectImplementation()
    {

        Destroy(gameObject);
    }



}