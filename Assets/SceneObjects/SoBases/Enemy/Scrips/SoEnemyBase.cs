using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnemyTypes/SoEnemyBase")]
public class SoEnemyBase : SoSceneObjectBase
{
    public int health;
    public bool permanent;
    public SoDamageEffect soDamageEffect;
    protected override Stats GetStatsInernal(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, health);
        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        EnemyBase enemyBase = sceneObject as EnemyBase;
        enemyBase.permanent = permanent;



    }
}