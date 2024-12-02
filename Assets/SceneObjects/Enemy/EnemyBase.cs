using System;
using UnityEngine;

public class EnemyBase : StaticSceneObject, IDamageable
{
    private BasicHealthSystem protectedHealthsystem;
    private bool isDead;
    public SoEnemyBase soEnemyBase;
    public HealthSystem healthSystem { get { return protectedHealthsystem; } set { } }

    public event EventHandler<IdamageAbleArgs> OnDeath;
    public iDamageableTypeEnum damageableType { get { return iDamageableTypeEnum.enemyBase; } }
  

    public SceneObject sceneObject { get { return this; } }

    public static EnemyBase Create(Vector3 position, SoEnemyBase _soEnemyBase )
    {
        EnemyBase enemyBase = _soEnemyBase.Init(position) as EnemyBase;
        enemyBase.SetHealthSystem(enemyBase.GetComponent<HealthSystem>());
        enemyBase.healthSystem.SetInitialHealth(_soEnemyBase.health);
        enemyBase.SetStats(_soEnemyBase.GetStats());
        enemyBase.OnCreated();
        return enemyBase;

    }
    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            OnDeath.Invoke(this,new IdamageAbleArgs { damageable=this});
        }
        
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
        protectedHealthsystem = healthSystem as BasicHealthSystem;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead())
        {
            return;
        }

        bool hasDied = healthSystem.TakeDamage(amount);
        if (hasDied)
        {
            Die();
        }
    }

    protected override void OnObjectDestroyed()
    {
        BattleSceneActions.OnEnemyBaseDestroyed();
    }

    public void OnCreated()
    {
        BattleSceneActions.OnDamagableCreated(this);
    }
}