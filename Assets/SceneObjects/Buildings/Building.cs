using System;
using Pathfinding;
using UnityEngine;
[RequireComponent(typeof(HealthSystem))]

public abstract class Building : StaticSceneObject, IDamageAble, IHasLifeSpan
{
    private HealthSystem Healthsystem;

    private TimeLimterSceneObject timeLimiter;
    public SceneObject sceneObject { get { return this; } }
    public virtual SceneObjectTypeEnum damageableType { get { return SceneObjectTypeEnum.playerbuilding; } }

    public IdamagableComponent idamageableComponent { get; set; }

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
            DestroySceneObject();
        }

    }
    #endregion
    protected override void OnObjectDestroyed()
    {

        Destroy(gameObject);
    }

    protected override void AddStatsForClick(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health,idamageableComponent.healthSystem.health);
    }

}