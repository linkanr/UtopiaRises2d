using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnemyTypes/SoEnemyBase")]
public class SoEnemyBase : SoSceneObjectBase
{
    public int health;
    public Sprite sprite;
    protected override Stats GetStatsInernal(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.health, health);
        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        
    }
}