using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Follower : SceneObject,  IDamageable
{
    public HealthSystem healthSystem { get { return protectedHelthSystem; } }
    bool isDead;
    public event EventHandler<IdamageAbleArgs> OnDeath;


    private HealthSystem protectedHelthSystem;
    public SceneObject sceneObject { get { return this; } }

    public iDamageableTypeEnum damageableType { get { return iDamageableTypeEnum.follower; } }
    

  

    private void Awake()
    {
        SetHealthSystem(GetComponent<HealthSystem>());

    }
    protected override void Start()
    {
        base.Start();
        
 
        OnCreated();

    }

    public Transform GetTransform()
    {
        return transform;
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
        OnDeath?.Invoke(this, new IdamageAbleArgs { damageable = this });
        BattleSceneActions.OnDamagableDestroyed(this);

        Destroy(gameObject);
    }

    public void OnCreated()
    {
        BattleSceneActions.OnDamagableCreated(this);
    }


}
