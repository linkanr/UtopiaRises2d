using Pathfinding;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MovingSceneObject, IDamageAble
{

    [HideInInspector]

    public TargeterForEnemies targeter;
    public Mover mover;
    public IdamagableComponent idamageableComponent { get; set; }
    public SceneObject sceneObject { get { return this; } }
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
