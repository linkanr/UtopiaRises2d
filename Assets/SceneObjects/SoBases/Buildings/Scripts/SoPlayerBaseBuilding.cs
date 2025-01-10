using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// SoShoting building adds attacksystem to the building so and must correspond to a shoting building scene object
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/SoBuildnings/PlayerBase")]
public class SoPlayerBaseBuilding : SoSceneObjectBase
{

    public int health;


    protected override Stats GetStatsInernal(Stats stats)
    {


        stats.Add(StatsInfoTypeEnum.health, health);
        stats.Add(StatsInfoTypeEnum.sceneObjectType, SceneObjectTypeEnum.playerBase);
        return stats;
    }

    protected override void ObjectInitialization(SceneObject sceneObject)
    {
        
    }
}