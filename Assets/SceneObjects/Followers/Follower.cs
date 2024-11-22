using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Follower : SceneObject, ITargetableByEnemy, IDamageable
{
    public HealthSystem healthSystem { get { return protectedHelthSystem; } }
    bool isDead;
    public event EventHandler<OnDeathArgs> OnDeath;
    private HealthSystem protectedHelthSystem;
    private void Awake()
    {
        SetHealthSystem(GetComponent<HealthSystem>());

    }
    protected override void Start()
    {
        base.Start();
        OnTargetableByEnemyCreated();
        OnFollowerCreated();   

    }





    private void OnFollowerDestroyed()
    {
        BattleSceneActions.OnFollowerDestroyed(transform);
    }
    private void OnFollowerCreated()
    {
        BattleSceneActions.OnFollowerCreated(transform);
    }
    public TargetableAttacksEnums TargatebleEnum()
    {
        return TargetableAttacksEnums.Follower;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void OnTargetableByEnemyCreated()
    {
        BattleSceneActions.OnTargetableCreated(this);
    }

    public void OnTargetableByEnemyDestroyed()
    {
        BattleSceneActions.OnTargetableDestroyed(this);
    }

    public void TakeDamage(int amount)
    {
        
        Die();

    }


    public void SetHealthSystem(HealthSystem healthSystem)
    {
        protectedHelthSystem = healthSystem;
    }

    public void Die()
    {
        if (IsDead())
        {
           return;
        }
        DestroySceneObject();
    }

    public bool IsDead()
    {
        return isDead;
    }

    protected override void OnObjectDestroyed()
    {
        isDead = true;
        OnDeath?.Invoke(this, new OnDeathArgs { damageable = this });
        OnTargetableByEnemyDestroyed();
        OnFollowerDestroyed();
        Destroy(gameObject);
    }


}
