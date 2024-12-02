using System;
using Pathfinding;
using UnityEngine;
[RequireComponent(typeof(HealthSystem))]

public abstract class Building:StaticSceneObject,IDamageable
{
    private HealthSystem Healthsystem;
    protected bool isDead = false;

    public HealthSystem healthSystem { get { return Healthsystem; } }

    public event EventHandler<IdamageAbleArgs> OnDeath;


    public SceneObject sceneObject { get { return this; } }

    public iDamageableTypeEnum damageableType { get { return iDamageableTypeEnum.playerbuilding; } }


    protected virtual void Awake()
    {
        
        Healthsystem = GetComponent<BasicHealthSystem>();
    }
    protected override void Start()
    {
        base.Start();
        OnCreated();
    }

    public void OnCreated()
    {
        BattleSceneActions.OnDamagableCreated(this);
    }



    public void Die()
    {
        if (IsDead())
        {
            return;
        }

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

    public void SetHealthSystem(HealthSystem healthSystem)
    {
        healthSystem = Healthsystem;
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


}