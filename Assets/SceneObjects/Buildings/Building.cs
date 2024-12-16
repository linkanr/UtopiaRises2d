using System;
using Pathfinding;
using UnityEngine;
[RequireComponent(typeof(HealthSystem))]

public abstract class Building : StaticSceneObject, IDamageable
{
    private HealthSystem Healthsystem;
    protected bool isDead = false;

    public virtual HealthSystem healthSystem { get { return Healthsystem; } }

    public event EventHandler<IdamageAbleArgs> OnDeath;


    public SceneObject sceneObject { get { return this; } }

    public virtual iDamageableTypeEnum damageableType { get { return iDamageableTypeEnum.playerbuilding; } }



    protected virtual void Awake()
    {

        SetHealthSystem(GetComponent<HealthSystem>());
    }
    protected override void Start()
    {
        base.Start();
        OnCreated();
    }

    public virtual void OnCreated()
    {
        BattleSceneActions.OnDamagableCreated(this);
    }



    public void Die()
    {
        if (IsDead())
        {
            return;
        }
        isDead = true;
        DestroySceneObject();

    }

    public Transform GetTransform()
    {
        if (isDead)
        {
            return null;
        }
        return transform;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void SetHealthSystem(HealthSystem _healthSystem)
    {
         Healthsystem =_healthSystem;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead())
        {
            return;
        }
        bool isDead = healthSystem.TakeDamage(amount);
        if (isDead)
        {
            Die();
        }
    }

    protected override void OnObjectDestroyed()
    {
        isDead = true;
        OnDeath?.Invoke(this, new IdamageAbleArgs { damageable = this });
        BattleSceneActions.OnDamagableDestroyed(this);
        Destroy(gameObject);
    }

    protected override void AddStatsForClick(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, healthSystem.health);
    }

}