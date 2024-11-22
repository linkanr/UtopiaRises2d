using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDamageable, ITargetableByEnemy

{

    private BasicHealthSystem protectedHealthsystem;

    public HealthSystem healthSystem { get { return protectedHealthsystem; } }

    public event EventHandler<OnDeathArgs> OnDeath;

    private void Start()
    {
       OnTargetableByEnemyCreated();
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
        OnTargetableByEnemyDestroyed();
    }

    public bool IsDead()
    {
       if (healthSystem.GetHealth() < 1)
        {
            return true;
        }
       return false;    
    }

    public void OnTargetableByEnemyDestroyed()
    {
        BattleSceneActions.OnTargetableDestroyed(this);
    }

    public void OnTargetableByEnemyCreated()
    {
        BattleSceneActions.OnTargetableCreated(this);
    }

    public TargetableAttacksEnums TargatebleEnum()
    {
        return TargetableAttacksEnums.Base;
    }
}
