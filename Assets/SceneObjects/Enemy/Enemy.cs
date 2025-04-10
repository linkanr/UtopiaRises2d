using Pathfinding;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MovingSceneObject,  IcanAttack
{

    [HideInInspector]

    public TargeterBaseClass targeter { get;set; }
    public AIPathVisualizer aIPathVisualizer;
    public Mover mover;

    public SceneObject sceneObject { get { return this; } }

    public override void OnCreated ()
    {
        EnemyManager.Instance.spawnedEnemiesList.Add(this);
    }

    protected override void OnObjectDestroyedObjectImplementation()
    {
        EnemyManager.Instance.spawnedEnemiesList.Remove(this);
        
    }
    protected override void AddStatsForClick(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health,healthSystem.GetHealth());


    }
}



