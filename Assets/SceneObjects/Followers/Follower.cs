using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Follower : SceneObject
{







    public SceneObjectTypeEnum damageableType { get { return SceneObjectTypeEnum.follower; } }



    protected override void Awake()
    {
        base.Awake();


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

       

        
    }


    protected override void AddStatsForClick(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, healthSystem.GetHealth());
    }

    public override void OnCreated()
    {
      
    }
}
