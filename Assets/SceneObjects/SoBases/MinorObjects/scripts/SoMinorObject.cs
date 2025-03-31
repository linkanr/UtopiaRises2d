using UnityEngine;

public class SoMinorObject : SoSceneObjectBase
{
    public int health;
    protected override Stats GetStatsInernal(Stats stats)
    {

        stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.minorObject);
        stats.Add(StatsInfoTypeEnum.health, health);
        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
  

        
        sceneObject.transform.SetParent(GameObject.Find("PersistantParent").transform);
    }

}