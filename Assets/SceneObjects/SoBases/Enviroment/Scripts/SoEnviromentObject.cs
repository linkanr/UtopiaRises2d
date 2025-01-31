using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnviromentTypes/SoEnviroment")]
public class SoEnviromentObject : SoSceneObjectBase
{
  
    public float moveFactor;
    protected override Stats GetStatsInernal(Stats stats)
    {
        stats.Add(StatsInfoTypeEnum.moveFactor, moveFactor);
        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        sceneObject.transform.SetParent(GameObject.Find("PersistantParent").transform);
    }
}
