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
    public IdamagableComponent idamageableComponent { get; set; }
    public SceneObject sceneObject { get { return this; } }

    public DamagerBaseClass damageDealer 
    { 
        get 
        { return GetStats().damagerBaseClass;}
        set {; }
    }

    protected override void Start()
    {
        base.Start();
        EnemyManager.Instance.spawnedEnemiesList.Add(this);
    }
    protected override void OnObjectDestroyed()
    {
        EnemyManager.Instance.spawnedEnemiesList.Remove(this);
        Destroy(gameObject);
    }
    protected override void AddStatsForClick(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, idamageableComponent.healthSystem.GetHealth());


    }
}



