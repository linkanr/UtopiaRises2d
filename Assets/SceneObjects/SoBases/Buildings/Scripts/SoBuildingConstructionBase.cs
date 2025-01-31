using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// SoShoting building adds attacksystem to the building so and must correspond to a shoting building scene object
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/SoBuildnings/SoBuildingConstructionBase")]
public class SoBuildingConstructionBase : SoSceneObjectBase
{




    protected override Stats GetStatsInernal(Stats stats)
    {



        stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.playerConstructionBase);
        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        
    }
}