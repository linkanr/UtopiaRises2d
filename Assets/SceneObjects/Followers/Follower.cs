using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Follower : SceneObject,  IDamageAble
{







    public SceneObjectTypeEnum damageableType { get { return SceneObjectTypeEnum.follower; } }

    public IDamagableComponent iDamageableComponent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    protected override void Awake()
    {
        base.Awake();


    }
    protected override void Start()
    {
        base.Start();
        
 


    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void TakeDamage(int amount)
    {
        
        Die();

    }


    public void Die()
    {
        if (isDead)
        {
           return;
        }
        
        isDead = true;
        OnSceneObjectDestroyedBase();
    }



    protected override void OnObjectDestroyedObjectImplementation()
    {
        isDead = true;

        BattleSceneActions.OnSceneObjectDestroyed(this);

        Destroy(gameObject);
    }


    protected override void AddStatsForClick(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, (iDamageableComponent as IdamagablePhysicalComponent).healthSystem.health);
    }
}
