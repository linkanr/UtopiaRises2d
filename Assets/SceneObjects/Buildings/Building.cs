using System;
using Pathfinding;
using UnityEngine;
[RequireComponent(typeof(HealthSystem))]

public abstract class Building:StaticSceneObject,IDamageable
{
    private HealthSystem Healthsystem;
    private bool isDead = false;

    public HealthSystem healthSystem { get { return Healthsystem; } }

    public event EventHandler<OnDeathArgs> OnDeath;


    


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
        OnDeath?.Invoke(this, new OnDeathArgs { damageable = this });
        Destroy(gameObject);
    }


    protected virtual void Awake()
    {
        
        Healthsystem = GetComponent<BasicHealthSystem>();
    }

}