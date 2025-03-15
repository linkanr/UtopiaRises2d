using System;
using UnityEngine;

public class EnemyBase : StaticSceneObject, IDamageAble
{

    public SceneObjectTypeEnum damageableType { get { return SceneObjectTypeEnum.enemyBase; } }

    public IDamagableComponent iDamageableComponent { get; set; }

    public static EnemyBase Create(Vector3 position, SoEnemyBase _soEnemyBase )
    {
        EnemyBase enemyBase = _soEnemyBase.Init(position) as EnemyBase;
       
        enemyBase.SetStats(_soEnemyBase.GetStats());
        return enemyBase;

    }

    protected override void OnObjectDestroyedObjectImplementation()
    {
        BattleSceneActions.OnEmemyDefeated?.Invoke();
    }


    protected override void AddStatsForClick(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, (iDamageableComponent as IdamagablePhysicalComponent).healthSystem.health);
    }
}