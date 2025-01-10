using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnemyTypes/SoEnemyBase")]
public class SoEnemyBase : SoSceneObjectBase
{
    public int health;

    protected override Stats GetStatsInernal(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, health);
        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        
    }
}