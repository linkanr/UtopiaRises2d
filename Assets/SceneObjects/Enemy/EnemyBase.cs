using System;
using UnityEngine;

public class EnemyBase : StaticSceneObject, IDamageAble
{

    public SceneObjectTypeEnum damageableType { get { return SceneObjectTypeEnum.enemyBase; } }

    public IdamagableComponent idamageableComponent { get; set; }

    public static EnemyBase Create(Vector3 position, SoEnemyBase _soEnemyBase )
    {
        EnemyBase enemyBase = _soEnemyBase.Init(position) as EnemyBase;
       
        enemyBase.idamageableComponent.healthSystem.SetInitialHealth(_soEnemyBase.health);
        enemyBase.SetStats(_soEnemyBase.GetStats());
        return enemyBase;

    }

    protected override void OnObjectDestroyed()
    {
        BattleSceneActions.OnEnemyBaseDestroyed?.Invoke();
    }


    protected override void AddStatsForClick(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, idamageableComponent.healthSystem.health);
    }
}