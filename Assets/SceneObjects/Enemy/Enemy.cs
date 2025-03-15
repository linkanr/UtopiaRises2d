using Pathfinding;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MovingSceneObject, IDamageAble, IcanAttack
{

    [HideInInspector]

    public TargeterBaseClass targeter { get;set; }
    public Mover mover;
    public IDamagableComponent iDamageableComponent { get; set; }
    public SceneObject sceneObject { get { return this; } }

    protected override void Start()
    {
        base.Start();
        iDamageableComponent = GetComponent<IdamagablePhysicalComponent>();
        if (iDamageableComponent == null)
        {
            Debug.LogError($"Enemy {name} has no IdamagableComponent!");
        }

        EnemyManager.Instance.spawnedEnemiesList.Add(this);

    }
    protected override void OnObjectDestroyedObjectImplementation()
    {
        EnemyManager.Instance.spawnedEnemiesList.Remove(this);
        Destroy(gameObject);
    }
    protected override void AddStatsForClick(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, (iDamageableComponent as IdamagablePhysicalComponent).healthSystem.GetHealth());


    }
}



