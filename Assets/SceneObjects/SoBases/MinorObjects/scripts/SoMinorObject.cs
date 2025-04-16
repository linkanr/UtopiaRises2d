using UnityEngine;

public class SoMinorObject : SoSceneObjectBase
{
    public float moveFactor;
    /// <summary>
    /// Burn time in seconds. -1 means not burnable.
    /// </summary>
    public bool addFuelToFire;
    public int health;
    public int damageWhenDestroyed;
    public float damageWhenDestroyedRadius;


    protected override Stats GetStatsInernal(Stats stats)
    {

        stats.Add(StatsInfoTypeEnum.moveFactor, moveFactor);
        stats.Add(StatsInfoTypeEnum.addFuelToFire, addFuelToFire);
        stats.Add(StatsInfoTypeEnum.health, health);
        stats.Add(StatsInfoTypeEnum.damageWhenDestroyed, damageWhenDestroyed);
        stats.Add(StatsInfoTypeEnum.damageWhenDiedRadius, damageWhenDestroyedRadius);
        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
  

        
        sceneObject.transform.SetParent(GameObject.Find("PersistantParent").transform);
    }

}