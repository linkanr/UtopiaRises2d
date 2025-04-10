using System;
using UnityEngine;

public class EnemyBase : StaticSceneObject
{
    public Action Onkilled;
    public SceneObjectTypeEnum damageableType { get { return SceneObjectTypeEnum.enemyBase; } }
    public EnemySpawner spawner;
    public HealthSystem iDamageableComponent { get; set; }
    public bool permanent;

    public static EnemyBase Create(Vector3 position, SoEnemyBase _soEnemyBase )
    {
        EnemyBase enemyBase = _soEnemyBase.Init(position) as EnemyBase;
       
        enemyBase.SetStats(_soEnemyBase.GetStats());
        return enemyBase;

    }

    protected override void OnObjectDestroyedObjectImplementation()
    {
        Onkilled();
        BattleSceneActions.OnEmemyDefeated?.Invoke();

    }

    protected override void AddStatsForClick(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, healthSystem.GetHealth());
    }

    public override void OnCreated()
    {
       
    }
}