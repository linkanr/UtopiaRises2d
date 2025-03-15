using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnviromentTypes/SoEnviroment")]
public class SoEnviromentObject : SoSceneObjectBase
{

    public float moveFactor;
    /// <summary>
    /// Burn time in seconds. -1 means not burnable.
    /// </summary>
    public bool addFuelToFire;
    public int health;
    protected override Stats GetStatsInernal(Stats stats)
    {
        
        stats.Add(StatsInfoTypeEnum.moveFactor, moveFactor);
        stats.Add(StatsInfoTypeEnum.addFuelToFire, addFuelToFire);
        stats.Add(StatsInfoTypeEnum.health, health);
        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        sceneObject.transform.SetParent(GameObject.Find("PersistantParent").transform);
    }
}
