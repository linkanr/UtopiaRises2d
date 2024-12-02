using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : StaticSceneObject, IDamageable

{

    private BasicHealthSystem protectedHealthsystem;

    public HealthSystem healthSystem { get { return protectedHealthsystem; } }

    public event EventHandler<IdamageAbleArgs> OnDeath;


    public iDamageableTypeEnum damageableType { get { return iDamageableTypeEnum.playerBase; } }
    public SceneObject sceneObject { get { return this; } }
    protected override void Start()
    {
       base.Start();
       OnCreated();
    }


    public void SetHealthSystem(HealthSystem healthSystem)
    {
        protectedHealthsystem = healthSystem as BasicHealthSystem;
    }
    public Transform GetTransform()
    {
        return transform;
    }



    public void TakeDamage(int amount)
    {
        HealthManager.Instance.TakeDamage(amount);
    }



    public void Die()
    {
        OnObjectDestroyed();
        BattleSceneActions.OnDamagableDestroyed(this);
    }

    public bool IsDead()
    {
       if (healthSystem.GetHealth() < 1)
        {
            return true;
        }
       return false;    
    }
    protected override void OnObjectDestroyed()
    {
        
    }

    public void OnCreated()
    {
  
        BattleSceneActions.OnDamagableCreated(this);
    }
}
