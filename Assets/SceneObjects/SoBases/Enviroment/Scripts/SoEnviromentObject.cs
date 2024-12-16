using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/EnviromentTypes/SoEnviroment")]
public class SoEnviromentObject : SoSceneObjectBase
{
  

    protected override Stats GetStatsInernal(Stats stats)
    {
        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        
    }
}
